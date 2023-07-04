using Comfy.Application.Common.Exceptions;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Orders;

public sealed record CreateOrderCommand : IRequest, IJwtValidation
{
    public Guid UserId { get; set; }

    public int[] ProductsIds { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Patronymic { get; set; } = null!;

    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;

    public string? UserComment { get; set; }
    public bool CallToConfirm { get; set; }
}


public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderStatus = await _context.OrderStatuses.FirstOrDefaultAsync(x => x.Name == OrderStatusEnum.Pending.ToString(), cancellationToken);
        if (orderStatus is null) throw new SomethingWrongException();

        var products = await _context.Products
            .Where(x => request.ProductsIds.Contains(x.Id))
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);

        if(products.Count != request.ProductsIds.Length) throw new SomeProductsAreNotAvailableException();
        var totalPrice = products.Sum(x => x.Price);

        var order = new Order
        {
            UserId = request.UserId,
            OrderStatusId = orderStatus.Id,
            TotalPrice = totalPrice,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Name = request.Name,
            Surname = request.Surname,
            Patronymic = request.Patronymic,
            City = request.City,
            Address = request.Address,
            UserComment = request.UserComment,
            CallToConfirm = request.CallToConfirm,
            Products = products
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
    }
}