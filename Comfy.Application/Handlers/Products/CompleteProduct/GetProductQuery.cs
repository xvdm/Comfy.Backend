using AutoMapper;
using Comfy.Application.Handlers.Products.CompleteProduct.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Products.CompleteProduct;

public record GetProductQuery(int ProductId) : IRequest<ProductDTO>, ICacheable
{
    public string CacheKey => $"product:{ProductId}";
    public double ExpirationHours => 12;
}


public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDTO> Handle(GetProductQuery request, CancellationToken cancellationToken)
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
            .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
            .Include(x => x.Reviews)
                .ThenInclude(x => x.Answers)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product is null) return null!;

        var result = _mapper.Map<Product, ProductDTO>(product);
        return result;
    }
}