using Comfy.Application.Common.Constants;
using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Authorization.Validators;

public sealed class CreatePendingUserCommandValidator : AbstractValidator<CreatePendingUserCommand>
{
    public CreatePendingUserCommandValidator(UserManager<User> userManager, IApplicationDbContext context)
    {
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Email).MustAsync(async (email, _) =>
        {
            var userWithEmail = await userManager.FindByEmailAsync(email);
            return userWithEmail is null;
        }).WithMessage(ValidationMessages.UserWithEmailAlreadyExists);

        RuleFor(x => x.Email).MustAsync(async (email, cancellationToken) =>
        {
            var pendingUsersCount = await context.PendingUsers.CountAsync(x => x.Email == email, cancellationToken);
            return pendingUsersCount <= 0;
        }).WithMessage(ValidationMessages.ConfirmationCodeWasAlreadySent);
    }
}