using FluentValidation;

namespace Comfy.Application.Handlers.Questions.QuestionAnswers.Validators;

public sealed class LikeQuestionAnswerCommandValidator : AbstractValidator<LikeQuestionAnswerCommand>
{
    public LikeQuestionAnswerCommandValidator()
    {
        RuleFor(x => x.QuestionAnswerId).GreaterThan(0);
    }
}