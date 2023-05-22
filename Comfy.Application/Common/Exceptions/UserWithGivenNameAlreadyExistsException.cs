namespace Comfy.Application.Common.Exceptions;

public class UserWithGivenNameAlreadyExistsException : Exception
{
    public UserWithGivenNameAlreadyExistsException() : base("Користувач з таким іменем вже існує.")
    {
    }
}