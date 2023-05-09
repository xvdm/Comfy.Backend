using System.Text.Json;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Comfy.Application.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheable
{
    private readonly IDistributedCache _distributedCache;

    public CachingBehavior(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response;
        var cachedValue = await _distributedCache.GetStringAsync(request.CacheKey, cancellationToken);
        if (string.IsNullOrEmpty(cachedValue))
        {
            response = await next.Invoke();
            cachedValue = JsonSerializer.Serialize(response);
            var options = new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromHours(request.ExpirationHours)
            };
            await _distributedCache.SetStringAsync(request.CacheKey, cachedValue, options, cancellationToken);
            return response;
        }

        response = JsonSerializer.Deserialize<TResponse>(cachedValue)!;
        return response;
    }
}