using FluentValidation;

namespace Comfy.Application.Handlers.Questions.Questions.Validators;

public sealed class LikeQuestionCommandValidator : AbstractValidator<LikeQuestionCommand>
{
    public LikeQuestionCommandValidator()
    {
        RuleFor(x => x.QuestionId).GreaterThan(0);
    }
}