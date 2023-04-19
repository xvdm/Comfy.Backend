using Comfy.Application.Interfaces;
using Comfy.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers;

public class GetProductsQuery : IRequest<IEnumerable<Product>>
{
    public int CategoryId { get; set; }
    public Dictionary<string, List<string>> QueryDictionary { get; set; }
    public GetProductsQuery(int categoryId, Dictionary<string, List<string>> queryDictionary)
    {
        CategoryId = categoryId;
        QueryDictionary = queryDictionary;
    }
}


public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = _context.Products
            .Where(x => x.CategoryId == request.CategoryId);

        if(request.QueryDictionary.Any())
        {
            products = products
                .Include(x => x.Model)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Characteristics);
        }

        products = products
            .AsNoTracking()
            .AsQueryable();

        foreach (var pair in request.QueryDictionary)
        {
            if (pair.Value.Any())
            {
                var ids = pair.Value.Where(x => int.TryParse(x, out var id)).Select(int.Parse).ToArray();
        
                if (pair.Key == "brand")
                {
                    products = products.Where(x => ids.Contains(x.Brand.Id));
                }
                else
                {
                    products = products.Where(x => x.Characteristics.Any(c => ids.Contains(c.CharacteristicsValueId)));
                }
            }
        }

        return await products.ToListAsync(cancellationToken);
    }
}
