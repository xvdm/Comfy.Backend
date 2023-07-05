namespace Comfy.Application.Common.Exceptions;

public sealed class ProductIsNotAvailableException : Exception
{
    public ProductIsNotAvailableException() : base("Цей товар наразі недоступний")
    {
    }
}