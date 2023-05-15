using FluentValidation;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.Validators;

public class GetProductsByQueryStringValidator : AbstractValidator<GetProductsByQueryString>
{
    public GetProductsByQueryStringValidator()
    {
        RuleFor(x => x.SubcategoryId).GreaterThan(0);
    }
}