using AutoMapper;
using Comfy.Application.Handlers.Products.CompleteProduct.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Products.CompleteProduct;

public sealed record GetProductByUrlQuery(string Url) : IRequest<ProductDTO>, ICacheable
{
    public string CacheKey => $"product:{Url}";
    public double ExpirationHours => 3;
}


public sealed class GetProductByUrlQueryHandler : IRequestHandler<GetProductByUrlQuery, ProductDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductByUrlQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDTO> Handle(GetProductByUrlQuery request, CancellationToken cancellationToken)
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
            .FirstOrDefaultAsync(x => x.Url == request.Url, cancellationToken);

        var result = _mapper.Map<Product, ProductDTO>(product!);
        return result;
    }
}