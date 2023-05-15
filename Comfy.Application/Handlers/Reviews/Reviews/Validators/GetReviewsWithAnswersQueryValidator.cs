using FluentValidation;

namespace Comfy.Application.Handlers.Reviews.Reviews.Validators;

public class GetReviewsWithAnswersQueryValidator : AbstractValidator<GetReviewsWithAnswersQuery>
{
    public GetReviewsWithAnswersQueryValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
    }
}