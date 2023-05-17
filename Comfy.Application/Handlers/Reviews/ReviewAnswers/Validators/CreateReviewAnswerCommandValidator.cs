using FluentValidation;

namespace Comfy.Application.Handlers.Reviews.ReviewAnswers.Validators;

public sealed class CreateReviewAnswerCommandValidator : AbstractValidator<CreateReviewAnswerCommand>
{
    public CreateReviewAnswerCommandValidator()
    {
        RuleFor(x => x.ReviewId).GreaterThan(0);
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.Text).NotEmpty();
    }
}