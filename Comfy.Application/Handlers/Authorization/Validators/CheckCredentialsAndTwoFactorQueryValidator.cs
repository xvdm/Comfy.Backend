using FluentValidation;

namespace Comfy.Application.Handlers.Authorization.Validators;

public sealed class CheckCredentialsAndTwoFactorQueryValidator : AbstractValidator<CheckCredentialsAndTwoFactorQuery>
{
    public CheckCredentialsAndTwoFactorQueryValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}