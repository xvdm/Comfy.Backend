using FluentValidation;

namespace Comfy.Application.Handlers.Users.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MinimumLength(6).MaximumLength(30);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(30);
    }
}