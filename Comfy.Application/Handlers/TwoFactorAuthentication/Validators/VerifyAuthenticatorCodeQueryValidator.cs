using FluentValidation;

namespace Comfy.Application.Handlers.TwoFactorAuthentication.Validators;

public sealed class VerifyAuthenticatorCodeQueryValidator : AbstractValidator<VerifyAuthenticatorCodeQuery>
{
    public VerifyAuthenticatorCodeQueryValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.Code).NotEmpty();
    }
}