using FluentValidation;

namespace Comfy.Application.Handlers.Reviews.Reviews.Validators;

public class DislikeReviewCommandValidator : AbstractValidator<DislikeReviewCommand>
{
    public DislikeReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId).GreaterThan(0);
    }
}