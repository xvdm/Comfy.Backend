namespace Comfy.Application.Common.Constants;

public sealed record ValidationMessages
{
    public const string UserWithEmailAlreadyExists = "Користувач з таким email вже існує.";
    public const string ConfirmationCodeWasAlreadySent = "На вказаний email вже надіслано код підтверждення.";
}