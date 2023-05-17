using FluentValidation;

namespace Comfy.Application.Handlers.Reviews.Reviews.Validators;

public sealed class DislikeReviewCommandValidator : AbstractValidator<DislikeReviewCommand>
{
    public DislikeReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId).GreaterThan(0);
    }
}