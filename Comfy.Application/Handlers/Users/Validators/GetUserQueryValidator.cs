using FluentValidation;

namespace Comfy.Application.Handlers.Users.Validators;

public sealed class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(Guid.Empty);
    }
}