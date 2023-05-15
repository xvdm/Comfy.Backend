using FluentValidation;

namespace Comfy.Application.Handlers.Reviews.ReviewAnswers.Validators;

public class LikeReviewAnswerCommandValidator : AbstractValidator<LikeReviewAnswerCommand>
{
    public LikeReviewAnswerCommandValidator()
    {
        RuleFor(x => x.ReviewAnswerId).GreaterThan(0);
    }
}