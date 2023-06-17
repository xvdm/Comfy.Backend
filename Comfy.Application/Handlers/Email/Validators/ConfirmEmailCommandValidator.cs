using FluentValidation;

namespace Comfy.Application.Handlers.Email.Validators;

public sealed class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.ConfirmationCode).NotEmpty();
    }
}