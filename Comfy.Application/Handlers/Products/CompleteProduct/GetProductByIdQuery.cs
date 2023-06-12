using AutoMapper;
using Comfy.Application.Handlers.Products.CompleteProduct.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Products.CompleteProduct;

public sealed record GetProductByIdQuery(int ProductId) : IRequest<ProductDTO>, ICacheable
{
    public string CacheKey => $"product:{ProductId}";
    public double ExpirationHours => 3;
}


public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(x => x.Brand)
            .Include(x => x.Category)
            .Include(x => x.Characteristics)
                .ThenInclude(x => x.CharacteristicsName)
            .Include(x => x.Characteristics)
                .ThenInclude(x => x.CharacteristicsValue)
            .Include(x => x.Images)
            .Include(x => x.Model)
            .Include(x => x.PriceHistory)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        var result = _mapper.Map<Product, ProductDTO>(product!);
        return result;
    }
}