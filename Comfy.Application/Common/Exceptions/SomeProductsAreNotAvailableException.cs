namespace Comfy.Application.Common.Exceptions;

public sealed class SomeProductsAreNotAvailableException : Exception
{
    public SomeProductsAreNotAvailableException() : base("Деякі товари недоступні")
    {
    }
}