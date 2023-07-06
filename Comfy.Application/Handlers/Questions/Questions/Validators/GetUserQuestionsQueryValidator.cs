using FluentValidation;

namespace Comfy.Application.Handlers.Questions.Questions.Validators;

public sealed class GetUserQuestionsQueryValidator : AbstractValidator<GetUserQuestionsQuery>
{
    public GetUserQuestionsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
    }
}