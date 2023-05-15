using FluentValidation;

namespace Comfy.Application.Handlers.Products.CompleteProduct.Validators;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
    }
}