using FluentValidation;

namespace Comfy.Application.Handlers.Reviews.Reviews.Validators;

public sealed class LikeReviewCommandValidator : AbstractValidator<LikeReviewCommand>
{
    public LikeReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId).GreaterThan(0);
    }
}