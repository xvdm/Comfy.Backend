using FluentValidation;

namespace Comfy.Application.Handlers.TwoFactorAuthentication.Validators;

public sealed class GetAuthenticatorSetupInfoQueryValidator : AbstractValidator<GetAuthenticatorSetupInfoQuery>
{
    public GetAuthenticatorSetupInfoQueryValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
    }
}