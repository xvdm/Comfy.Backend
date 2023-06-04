using FluentValidation;

namespace Comfy.Application.Handlers.Reviews.Reviews.Validators;

public sealed class GetReviewsWithAnswersQueryValidator : AbstractValidator<GetReviewsQuery>
{
    public GetReviewsWithAnswersQueryValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
    }
}