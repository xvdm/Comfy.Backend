using FluentValidation;

namespace Comfy.Application.Handlers.Products.CompleteProduct.Validators;

public sealed class GetProductByUrlQueryValidator : AbstractValidator<GetProductByUrlQuery>
{
    public GetProductByUrlQueryValidator()
    {
        RuleFor(x => x.Url).NotEmpty();
    }
}