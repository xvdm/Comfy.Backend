namespace Comfy.Application.Common.Exceptions;

public class ProviderMismatchException : Exception
{
    public ProviderMismatchException() : base("Цей Email використовується іншим провайдером.")
    {
    }
}