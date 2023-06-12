namespace Comfy.Application.Common.Exceptions;

public class UserEmailLockoutException : Exception
{
    public UserEmailLockoutException() : base("Email заблоковано.")
    {
    }
}