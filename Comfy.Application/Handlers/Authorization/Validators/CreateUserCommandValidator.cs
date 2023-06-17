using Comfy.Application.Common.Constants;
using Comfy.Application.Common.Exceptions;
using Comfy.Domain.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.Authorization.Validators;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(UserManager<User> userManager)
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(30);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(30);

        RuleFor(x => x.Email).MustAsync(async (email, _) =>
        {
            var userWithEmail = await userManager.FindByEmailAsync(email);
            if (userWithEmail is not null) throw new UserWithGivenEmailAlreadyExistsException();

            return true;
        }).WithMessage(ValidationMessages.UserWithEmailAlreadyExists);
    }
}