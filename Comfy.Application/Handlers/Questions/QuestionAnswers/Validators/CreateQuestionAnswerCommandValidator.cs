using FluentValidation;

namespace Comfy.Application.Handlers.Questions.QuestionAnswers.Validators;

public class CreateQuestionAnswerCommandValidator : AbstractValidator<CreateQuestionAnswerCommand>
{
    public CreateQuestionAnswerCommandValidator()
    {
        RuleFor(x => x.QuestionId).GreaterThan(0);
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.Text).NotEmpty();
    }
}