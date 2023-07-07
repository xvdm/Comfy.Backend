namespace Comfy.Application.Common.Constants;

public sealed record ValidationMessages
{
    // every message must have a dot (.) at the end
    public const string UserWithEmailAlreadyExists = "Користувач з таким email вже існує.";
    public const string ConfirmationCodeWasAlreadySent = "На вказаний email вже надіслано код підтверждення.";
    public const string QuestionWasNotFound = "Питання не знайдено.";
    public const string QuestionAnswerWasNotFound = "Відповідь на питання не знайдено.";
    public const string ReviewWasNotFound = "Відгук не знайдено.";
    public const string ReviewAnswerWasNotFound = "Відповідь на відгук не знайдено.";
    public const string ProductWasNotFound = "Товар не знайдено.";
}