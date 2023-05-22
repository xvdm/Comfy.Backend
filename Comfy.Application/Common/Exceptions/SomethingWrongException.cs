namespace Comfy.Application.Common.Exceptions;

public sealed class SomethingWrongException : Exception
{
    public SomethingWrongException() : base("Щось пішло не так.")
    {
    }
}