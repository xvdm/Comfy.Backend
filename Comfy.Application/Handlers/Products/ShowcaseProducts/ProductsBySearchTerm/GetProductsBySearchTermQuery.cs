using AutoMapper;
using Comfy.Application.Handlers.Products.DTO;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsBySearchTerm.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsBySearchTerm;

public sealed record GetProductsBySearchTermQuery : IRequest<ProductsPageBySearchTermDTO>
{
    public string SearchTerm { get; init; }
    public string? SortColumn { get; init; }
    public string? SortOrder { get; init; }
    public int? PriceFrom { get; init; }
    public int? PriceTo { get; init; }


    private const int MaxPageSize = 50;
    private int _pageSize = MaxPageSize;
    private int _pageNumber = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value is > MaxPageSize or < 1 ? MaxPageSize : value;
    }
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    public GetProductsBySearchTermQuery(string searchTerm, int? priceFrom, int? priceTo, string? sortColumn, string? sortOrder, int? pageNumber, int? pageSize)
    {
        SearchTerm = searchTerm.Trim();
        PriceFrom = priceFrom;
        PriceTo = priceTo;
        SortColumn = sortColumn?.Trim().ToLower();
        SortOrder = sortOrder?.Trim().ToLower();
        if (pageNumber is not null) PageNumber = (int)pageNumber;
        if (pageSize is not null) PageSize = (int)pageSize;
    }
}


public sealed class GetProductsBySearchTermQueryHandler : IRequestHandler<GetProductsBySearchTermQuery, ProductsPageBySearchTermDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsBySearchTermQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductsPageBySearchTermDTO> Handle(GetProductsBySearchTermQuery request, CancellationToken cancellationToken)
    {
        var products = _context.Products
            .Include(x => x.Images.OrderBy(y => y.Id).Take(3))
            .Include(x => x.Category)
            .Include(x => x.CharacteristicGroups.OrderBy(y => y.Id).Take(1))
                .ThenInclude(x => x.Characteristics.Take(5))
                    .ThenInclude(x => x.CharacteristicsName)
            .Include(x => x.CharacteristicGroups)
                .ThenInclude(x => x.Characteristics)
                    .ThenInclude(x => x.CharacteristicsValue)
            .Where(x => x.IsActive == true)
            .Where(x => x.Name.Contains(request.SearchTerm))
            .AsNoTracking()
            .AsQueryable();

        if (request.PriceFrom is not null) products = products.Where(x => x.Price >= request.PriceFrom);
        if (request.PriceTo is not null) products = products.Where(x => x.Price <= request.PriceTo);

        if (request.SortOrder == "desc") products = products.OrderByDescending(GetSortProperty(request));
        else products = products.OrderBy(GetSortProperty(request));

        var productsNumber = await products.CountAsync(cancellationToken);

        var productsList = await products
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mappedProducts = _mapper.Map<IEnumerable<ShowcaseProductDTO>>(productsList);

        var result = new ProductsPageBySearchTermDTO
        {
            Products = mappedProducts,
            TotalProductsNumber = productsNumber
        };
        return result;
    }

    private static Expression<Func<Product, object>> GetSortProperty(GetProductsBySearchTermQuery request)
    {
        return request.SortColumn switch
        {
            "name" => x => x.Name,
            "price" => x => x.Price,
            "rating" => x => x.Rating,
            _ => x => x.Id
        };
    }
} 