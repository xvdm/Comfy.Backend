namespace Comfy.Application.Common.Exceptions;

public sealed class EmailServiceException : Exception
{
    public EmailServiceException() : base("Щось пішло не так у сервісі Email")
    {
    }
}