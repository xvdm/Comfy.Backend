using FluentValidation;

namespace Comfy.Application.Handlers.Questions.Questions.Validators;

public class GetQuestionsWithAnswersQueryValidator : AbstractValidator<GetQuestionsWithAnswersQuery>
{
    public GetQuestionsWithAnswersQueryValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
    }
}