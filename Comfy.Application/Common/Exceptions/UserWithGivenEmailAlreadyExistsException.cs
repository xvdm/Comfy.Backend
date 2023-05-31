namespace Comfy.Application.Common.Exceptions;

public class UserWithGivenEmailAlreadyExistsException : Exception
{
    public UserWithGivenEmailAlreadyExistsException() : base("Користувач з таким e-mail вже існує.")
    {
    }
}