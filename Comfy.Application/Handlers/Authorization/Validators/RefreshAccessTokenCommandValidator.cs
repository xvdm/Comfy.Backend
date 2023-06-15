using FluentValidation;

namespace Comfy.Application.Handlers.Authorization.Validators;

public sealed class RefreshAccessTokenCommandValidator : AbstractValidator<RefreshAccessTokenCommand>
{
    public RefreshAccessTokenCommandValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.RefreshToken).NotEqual(Guid.Empty);
        RuleFor(x => x.AccessToken).NotEmpty();
    }
}