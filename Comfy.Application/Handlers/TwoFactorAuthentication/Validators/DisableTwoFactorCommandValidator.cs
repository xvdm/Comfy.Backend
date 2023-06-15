using FluentValidation;

namespace Comfy.Application.Handlers.TwoFactorAuthentication.Validators;

public sealed class DisableTwoFactorCommandValidator : AbstractValidator<DisableTwoFactorCommand>
{
    public DisableTwoFactorCommandValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
    }
}