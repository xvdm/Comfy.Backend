﻿using FluentValidation;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByIds.Validators;

public sealed class GetProductsByIdsQueryValidator : AbstractValidator<GetProductsByIdsQuery>
{
    public GetProductsByIdsQueryValidator()
    {
        RuleFor(x => x.Ids).NotEmpty();
        RuleForEach(x => x.Ids).GreaterThan(0);
    }
}