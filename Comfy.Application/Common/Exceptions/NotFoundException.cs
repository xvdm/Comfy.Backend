namespace Comfy.Application.Common.Exceptions;

public sealed class NotFoundException : Exception
{
    //public NotFoundException(string name, object key) : base($"{name} ({key}) не знайдено")
    public NotFoundException(string name) : base($"{name} не знайдено")
    {
    }
}