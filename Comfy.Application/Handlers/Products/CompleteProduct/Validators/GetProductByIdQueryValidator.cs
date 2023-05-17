using FluentValidation;

namespace Comfy.Application.Handlers.Products.CompleteProduct.Validators;

public sealed class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
    }
}