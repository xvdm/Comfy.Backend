using FluentValidation;

namespace Comfy.Application.Handlers.Questions.Questions.Validators;

public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.Text).NotEmpty();
    }
}