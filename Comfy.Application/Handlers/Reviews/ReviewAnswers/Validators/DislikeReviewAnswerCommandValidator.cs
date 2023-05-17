using FluentValidation;

namespace Comfy.Application.Handlers.Reviews.ReviewAnswers.Validators;

public sealed class DislikeReviewAnswerCommandValidator : AbstractValidator<DislikeReviewAnswerCommand>
{
    public DislikeReviewAnswerCommandValidator()
    {
        RuleFor(x => x.ReviewAnswerId).GreaterThan(0);
    }
}