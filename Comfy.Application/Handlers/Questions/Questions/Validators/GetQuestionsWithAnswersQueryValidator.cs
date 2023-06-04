using FluentValidation;

namespace Comfy.Application.Handlers.Questions.Questions.Validators;

public sealed class GetQuestionsWithAnswersQueryValidator : AbstractValidator<GetQuestionsQuery>
{
    public GetQuestionsWithAnswersQueryValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
    }
}