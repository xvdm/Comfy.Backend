using Comfy.Application.Common.Constants;
using Comfy.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Questions.QuestionAnswers.Validators;

public sealed class CreateQuestionAnswerCommandValidator : AbstractValidator<CreateQuestionAnswerCommand>
{
    public CreateQuestionAnswerCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.QuestionId).GreaterThan(0);
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.Text).NotEmpty();

        RuleFor(x => x.QuestionId).MustAsync(async (id, cancellationToken) =>
        {
            return await context.Questions.AnyAsync(x => x.Id == id, cancellationToken);
        }).WithMessage(ValidationMessages.QuestionWasNotFound);
    }
}