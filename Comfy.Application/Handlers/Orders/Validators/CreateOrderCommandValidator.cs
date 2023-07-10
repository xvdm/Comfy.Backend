using FluentValidation;

namespace Comfy.Application.Handlers.Orders.Validators;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Patronymic).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.ProductsIds).NotEmpty();
        RuleForEach(x => x.ProductsIds).GreaterThan(0);
    }
}
