using FluentValidation;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.Validators;

public sealed class GetProductsByQueryStringValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsByQueryStringValidator()
    {
        RuleFor(x => x.SubcategoryId).GreaterThan(0);
    }
}