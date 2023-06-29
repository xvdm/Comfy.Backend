using FluentValidation;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.Validators;

public sealed class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.SubcategoryId).GreaterThan(0);
        RuleFor(x => x.PriceFrom).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PriceTo).GreaterThanOrEqualTo(0);
    }
}