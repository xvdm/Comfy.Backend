using FluentValidation;

namespace Comfy.Application.Handlers.Authorization.Validators;

public sealed class CreatePendingUserCommandValidator : AbstractValidator<CreatePendingUserCommand>
{
    public CreatePendingUserCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
    }
}