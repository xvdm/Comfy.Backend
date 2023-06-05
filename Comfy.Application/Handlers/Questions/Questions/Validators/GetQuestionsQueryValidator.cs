using FluentValidation;

namespace Comfy.Application.Handlers.Questions.Questions.Validators;

public sealed class GetQuestionsQueryValidator : AbstractValidator<GetQuestionsQuery>
{
    public GetQuestionsQueryValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
    }
}