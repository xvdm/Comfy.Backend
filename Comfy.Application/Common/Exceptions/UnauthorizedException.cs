namespace Comfy.Application.Common.Exceptions;

public sealed class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Спроба виконати несанкціоновану операцію.")
    {
    }
}