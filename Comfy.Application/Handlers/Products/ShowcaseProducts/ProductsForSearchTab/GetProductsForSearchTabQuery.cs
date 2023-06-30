using AutoMapper;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsForSearchTab.DTO;
using Comfy.Application.Interfaces;
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
            .AsNoTracking()
            .AsQueryable();

        if (string.IsNullOrEmpty(request.SearchTerm) == false)
        {
            productsQueryable = productsQueryable.Where(x => x.Name.Contains(request.SearchTerm));
        }

        productsQueryable = productsQueryable
            .OrderBy(x => x.Rating)
            .Take(4);

        var productsList = await productsQueryable.ToListAsync(cancellationToken);

        var mappedProducts = _mapper.Map<IEnumerable<SearchTabProductDTO>>(productsList);
        return mappedProducts;
    }
} 