using FluentValidation;

namespace Comfy.Application.Handlers.Authorization.Validators;

public sealed class SignInByPasswordCommandHandler : AbstractValidator<SignInByPasswordCommand>
{
    public SignInByPasswordCommandHandler()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}