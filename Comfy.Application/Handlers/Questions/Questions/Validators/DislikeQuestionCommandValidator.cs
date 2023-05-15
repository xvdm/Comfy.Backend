using FluentValidation;

namespace Comfy.Application.Handlers.Questions.Questions.Validators;

public class DislikeQuestionCommandValidator : AbstractValidator<DislikeQuestionCommand>
{
    public DislikeQuestionCommandValidator()
    {
        RuleFor(x => x.QuestionId).GreaterThan(0);
    }
}