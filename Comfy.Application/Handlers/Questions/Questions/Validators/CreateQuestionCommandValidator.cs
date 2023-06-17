using Comfy.Application.Common.Constants;
using Comfy.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Questions.Questions.Validators;

public sealed class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.Text).NotEmpty();

        RuleFor(x => x.ProductId).MustAsync(async (id, cancellationToken) =>
        {
            var products = await context.Products.CountAsync(x => x.Id == id, cancellationToken);
            return products > 0;
        }).WithMessage(ValidationMessages.QuestionAnswerWasNotFound);
    }
}