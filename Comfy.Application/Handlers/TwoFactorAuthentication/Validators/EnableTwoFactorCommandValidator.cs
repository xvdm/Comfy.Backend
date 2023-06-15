using FluentValidation;

namespace Comfy.Application.Handlers.TwoFactorAuthentication.Validators;

public sealed class EnableTwoFactorCommandValidator : AbstractValidator<EnableTwoFactorCommand>
{
    public EnableTwoFactorCommandValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
    }
}