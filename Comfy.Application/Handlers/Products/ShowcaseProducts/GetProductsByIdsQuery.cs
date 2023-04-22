using AutoMapper;
using Comfy.Application.Handlers.Products.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts;

public class GetProductsByIdsQuery : IRequest<ICollection<ShowcaseProductDTO>>
{
    public int[] Ids { get; set; }

    public GetProductsByIdsQuery(int[] ids)
    {
        Ids = ids;
    }
}

public class GetProductsByIdsQueryHandler : IRequestHandler<GetProductsByIdsQuery, ICollection<ShowcaseProductDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsByIdsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ShowcaseProductDTO>> Handle(GetProductsByIdsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .Where(x => request.Ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<ICollection<ShowcaseProductDTO>>(products);
        return result;
    }
}