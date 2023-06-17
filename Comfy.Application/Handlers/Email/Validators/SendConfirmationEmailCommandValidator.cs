using FluentValidation;

namespace Comfy.Application.Handlers.Email.Validators;

public sealed class SendConfirmationEmailCommandValidator :AbstractValidator<SendConfirmationEmailCommand>
{
    public SendConfirmationEmailCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.ConfirmationCode).NotEmpty();
    }
}