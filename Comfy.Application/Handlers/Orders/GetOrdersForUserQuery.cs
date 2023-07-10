using AutoMapper;
using Comfy.Application.Handlers.Orders.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Orders;

public sealed record GetOrdersForUserQuery(Guid UserId) : IRequest<IEnumerable<OrderDTO>>, IJwtValidation;


public sealed class GetOrdersForUserQueryHandler : IRequestHandler<GetOrdersForUserQuery, IEnumerable<OrderDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrdersForUserQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDTO>> Handle(GetOrdersForUserQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Include(x => x.OrderStatus)
            .Include(x => x.Products)
            .ThenInclude(x => x.Images.OrderBy(y => y.Id).Take(1))
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.Id)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var mappedOrders = _mapper.Map<IEnumerable<OrderDTO>>(orders);
        return mappedOrders;
    }
}