using FluentValidation;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsBySearchTerm.Validators;

public sealed class GetProductsBySearchTermQueryValidator : AbstractValidator<GetProductsBySearchTermQuery>
{
    public GetProductsBySearchTermQueryValidator()
    {
        RuleFor(x => x.SearchTerm).NotEmpty();
        RuleFor(x => x.PriceFrom).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PriceTo).GreaterThanOrEqualTo(0);
    }
}