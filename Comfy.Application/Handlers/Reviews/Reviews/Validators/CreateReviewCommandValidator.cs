using Comfy.Application.Common.Constants;
using Comfy.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews.Reviews.Validators;

public sealed class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.Text).NotEmpty();
        RuleFor(x => x.Advantages).NotEmpty();
        RuleFor(x => x.Disadvantages).NotEmpty();
        RuleFor(x => x.ProductRating).InclusiveBetween(1, 5);

        RuleFor(x => x.ProductId).MustAsync(async (id, cancellationToken) =>
        {
            return await context.Products.AnyAsync(x => x.Id == id, cancellationToken);
        }).WithMessage(ValidationMessages.ReviewAnswerWasNotFound);
    }
}