using FluentValidation;

namespace Comfy.Application.Handlers.Questions.Questions.Validators;

public sealed class DislikeQuestionCommandValidator : AbstractValidator<DislikeQuestionCommand>
{
    public DislikeQuestionCommandValidator()
    {
        RuleFor(x => x.QuestionId).GreaterThan(0);
    }
}