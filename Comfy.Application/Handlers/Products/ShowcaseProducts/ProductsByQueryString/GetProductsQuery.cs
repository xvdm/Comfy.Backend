using System.Linq.Expressions;
using AutoMapper;
using Comfy.Application.Common.Helpers;
using Comfy.Application.Handlers.Products.DTO;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString;

public sealed record GetProductsQuery : IRequest<ProductsPageDTO>
{
    public string? SearchTerm { get; init; }
    public string? QueryString { get; init; }
    public string? SortColumn { get; init; }
    public string? SortOrder { get; init; }
    public int SubcategoryId { get; init; }
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

    public GetProductsQuery(int subcategoryId, int? priceFrom, int? priceTo, string? searchTerm, string? sortColumn, string? sortOrder, string? queryString, int? pageNumber, int? pageSize)
    {
        SubcategoryId = subcategoryId;
        PriceFrom = priceFrom;
        PriceTo = priceTo;
        SearchTerm = searchTerm?.Trim();
        QueryString = queryString?.ToLower();
        SortColumn = sortColumn?.Trim().ToLower();
        SortOrder = sortOrder?.Trim().ToLower();
        if (pageNumber is not null) PageNumber = (int)pageNumber;
        if (pageSize is not null) PageSize = (int)pageSize;
    }
}


public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsPageDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductsPageDTO> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var queryString = request.QueryString;
        var changed = ProductUrl.RemoveEmptyAndDuplicatesFromQuery(queryString, out var queryDictionary);
        if (changed) queryString = ProductUrl.GetQueryStringFromDictionary(queryDictionary);

        var category = await _context.Subcategories
            .Include(x => x.UniqueCharacteristics)
                .ThenInclude(x => x.CharacteristicsName)
            .Include(x => x.UniqueCharacteristics)
                .ThenInclude(x => x.CharacteristicsValue)
            .Include(x => x.UniqueBrands)
            .FirstOrDefaultAsync(x => x.Id == request.SubcategoryId, cancellationToken);

        var result = new ProductsPageDTO
        {
            SubcategoryId = request.SubcategoryId,
            QueryString = queryString
        };

        if (category is null) return result;
        
        var products = _context.Products
            .Include(x => x.Images.OrderBy(y => y.Id).Take(3))
            .Include(x => x.Category)
            .Include(x => x.CharacteristicGroups.OrderBy(y => y.Id).Take(1))
                .ThenInclude(x => x.Characteristics.Take(5))
                    .ThenInclude(x => x.CharacteristicsName)
            .Include(x => x.CharacteristicGroups)
                .ThenInclude(x => x.Characteristics)
                    .ThenInclude(x => x.CharacteristicsValue)
            .Where(x => x.CategoryId == request.SubcategoryId)
            .Where(x => x.IsActive == true)
            .AsNoTracking()
            .AsQueryable();

        if (request.PriceFrom is not null) products = products.Where(x => x.Price >= request.PriceFrom);
        if (request.PriceTo is not null) products = products.Where(x => x.Price <= request.PriceTo);

        if (string.IsNullOrEmpty(request.SearchTerm) == false)
        {
            products = products.Where(x => x.Name.Contains(request.SearchTerm));
        }

        if (request.SortOrder == "desc")
        {
            products = products.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            products = products.OrderBy(GetSortProperty(request));
        }

        if (queryDictionary.Any())
        {
            products = products
                .Include(x => x.Model)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Characteristics);
        }

        foreach (var pair in queryDictionary.Where(pair => pair.Value.Any()))
        {
            var ids = pair.Value.Where(x => int.TryParse(x, out var id)).Select(int.Parse).ToArray();

            if (pair.Key == "brand")
            {
                products = products.Where(x => ids.Contains(x.BrandId));
            }
            else if (pair.Key == "model")
            {
                products = products.Where(x => ids.Contains(x.ModelId));
            }
            else
            {
                products = products.Where(x => x.Characteristics.Any(c => ids.Contains(c.CharacteristicsValueId)));
            }
        }

        var characteristics = GetCharacteristicsInCategory(category);
        var brands = category.UniqueBrands.ToList();

        var productsList = await products
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mappedProducts = _mapper.Map<IEnumerable<ShowcaseProductDTO>>(productsList);
        var mappedBrands = _mapper.Map<IEnumerable<BrandDTO>>(brands);

        result = new ProductsPageDTO
        {
            SubcategoryId = category.Id,
            QueryString = queryString,
            Characteristics = characteristics,
            Brands = mappedBrands,
            Products = mappedProducts
        };

        return result;
    }


    private static Expression<Func<Product, object>> GetSortProperty(GetProductsQuery request)
    {
        return request.SortColumn switch
        {
            "name" => x => x.Name,
            "price" => x => x.Price,
            "rating" => x => x.Rating,
            _ => x => x.Id
        };
    }


    private IEnumerable<CharacteristicDTO> GetCharacteristicsInCategory(Subcategory category)
    {
        var characteristicsList = new List<CharacteristicDTO>();
        foreach (var characteristic in category.UniqueCharacteristics)
        {
            var exists = false;
            foreach (var characteristicDTO in characteristicsList)
            {
                if (characteristicDTO.Name.Name == characteristic.CharacteristicsName.Name)
                {
                    var value = _mapper.Map<CharacteristicValueDTO>(characteristic.CharacteristicsValue);
                    characteristicDTO.Values.Add(value);
                    exists = true;
                    break;
                }
            }

            if (exists) continue;

            var newCharacteristicDTO = new CharacteristicDTO
            {
                Name = _mapper.Map<CharacteristicNameDTO>(characteristic.CharacteristicsName),
                Values = new List<CharacteristicValueDTO> { _mapper.Map<CharacteristicValueDTO>(characteristic.CharacteristicsValue) }
            };
            characteristicsList.Add(newCharacteristicDTO);
        }
        return characteristicsList;
    }
}