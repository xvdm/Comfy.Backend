using FluentValidation;

namespace Comfy.Application.Handlers.Questions.QuestionAnswers.Validators;

public sealed class DislikeQuestionAnswerCommandValidator : AbstractValidator<DislikeQuestionAnswerCommand>
{
    public DislikeQuestionAnswerCommandValidator()
    {
        RuleFor(x => x.QuestionAnswerId).GreaterThan(0);
    }
}