using Comfy.Application.Common.Exceptions;
using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Comfy.Application.Common.Behaviors;

public sealed class JwtValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IJwtValidation
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<User> _userManager;

    public JwtValidationBehavior(IHttpContextAccessor contextAccessor, UserManager<User> userManager)
    {
        _contextAccessor = contextAccessor;
        _userManager = userManager;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_contextAccessor.HttpContext is null) throw new UnauthorizedException();

        var sub = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        if (sub != request.UserId.ToString()) throw new UnauthorizedException();

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) throw new UnauthorizedException();

        var stamp = await _userManager.GetSecurityStampAsync(user);
        var tokenStamp = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
        if (tokenStamp is null || tokenStamp != stamp) throw new UnauthorizedException();

        return await next.Invoke();
    }
}