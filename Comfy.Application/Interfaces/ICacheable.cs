namespace Comfy.Application.Interfaces;

public interface ICacheable
{
    public string CacheKey { get; }
    public double ExpirationHours { get; }
}