using AutoMapper;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsForSearchTab.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsForSearchTab;

public sealed record GetProductsForSearchTabQuery(string? SearchTerm) : IRequest<IEnumerable<SearchTabProductDTO>>;


public sealed class GetProductsForSearchTabQueryHandler : IRequestHandler<GetProductsForSearchTabQuery, IEnumerable<SearchTabProductDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsForSearchTabQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SearchTabProductDTO>> Handle(GetProductsForSearchTabQuery request, CancellationToken cancellationToken)
    {
        var productsQueryable = _context.Products
            .OrderBy(x => x.Rating)
            .AsQueryable();

        if (string.IsNullOrEmpty(request.SearchTerm) == false)
        {
            productsQueryable = productsQueryable.Where(x => x.Name.ToLower().Contains(request.SearchTerm.ToLower()));
        }

        var productsList = await productsQueryable
            .Take(4)
            .Select(x => new SearchTabProductDTO
            {
                DiscountAmount = x.DiscountAmount,
                Name = x.Name,
                Price = x.Price,
                Url = x.Url,
                ImageUrl = x.Images.FirstOrDefault()!.Url
            })
            .ToListAsync(cancellationToken);

        var mappedProducts = _mapper.Map<IEnumerable<SearchTabProductDTO>>(productsList);
        return mappedProducts;
    }
} 