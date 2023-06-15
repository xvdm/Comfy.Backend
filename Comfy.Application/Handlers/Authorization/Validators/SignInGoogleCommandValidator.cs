using FluentValidation;

namespace Comfy.Application.Handlers.Authorization.Validators;

public sealed class SignInGoogleCommandValidator : AbstractValidator<SignInGoogleCommand>
{
    public SignInGoogleCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Subject).NotEmpty();
    }
}