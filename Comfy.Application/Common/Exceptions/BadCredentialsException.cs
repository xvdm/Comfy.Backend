namespace Comfy.Application.Common.Exceptions;

public sealed class BadCredentialsException : Exception
{
    public BadCredentialsException() : base("Невірні облікові дані")
    {
    }
}