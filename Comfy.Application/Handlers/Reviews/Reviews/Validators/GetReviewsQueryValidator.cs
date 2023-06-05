using FluentValidation;

namespace Comfy.Application.Handlers.Reviews.Reviews.Validators;

public sealed class GetReviewsQueryValidator : AbstractValidator<GetReviewsQuery>
{
    public GetReviewsQueryValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
    }
}