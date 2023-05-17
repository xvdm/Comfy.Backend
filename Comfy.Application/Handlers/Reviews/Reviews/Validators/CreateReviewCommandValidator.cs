using FluentValidation;

namespace Comfy.Application.Handlers.Reviews.Reviews.Validators;

public sealed class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.Text).NotEmpty();
        RuleFor(x => x.Advantages).NotEmpty();
        RuleFor(x => x.Disadvantages).NotEmpty();
        RuleFor(x => x.ProductRating).InclusiveBetween(1, 5);
    }
}