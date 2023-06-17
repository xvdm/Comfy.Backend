using Comfy.Application.Common.Constants;
using Comfy.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews.ReviewAnswers.Validators;

public sealed class CreateReviewAnswerCommandValidator : AbstractValidator<CreateReviewAnswerCommand>
{
    public CreateReviewAnswerCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.ReviewId).GreaterThan(0);
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.Text).NotEmpty();

        RuleFor(x => x.ReviewId).MustAsync(async (id, cancellationToken) =>
        {
            var reviews = await context.Reviews.CountAsync(x => x.Id == id, cancellationToken);
            return reviews > 0;
        }).WithMessage(ValidationMessages.ReviewWasNotFound);
    }
}