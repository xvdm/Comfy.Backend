﻿using AutoMapper;
using Comfy.Application.Handlers.Products.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByIds;

public sealed record GetProductsByIdsQuery(int[] Ids) : IRequest<IEnumerable<ShowcaseProductDTO>>;


public sealed class GetProductsByIdsQueryHandler : IRequestHandler<GetProductsByIdsQuery, IEnumerable<ShowcaseProductDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsByIdsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShowcaseProductDTO>> Handle(GetProductsByIdsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .Where(x => request.Ids.Contains(x.Id))
            .Where(x => x.IsActive == true)
            .Include(x => x.Images.OrderBy(y => y.Id).Take(3))
            .Include(x => x.Category)
            .Include(x => x.CharacteristicGroups.OrderBy(y => y.Id).Take(1))
                .ThenInclude(x => x.Characteristics.Take(5))
                    .ThenInclude(x => x.CharacteristicsName)
            .Include(x => x.CharacteristicGroups)
                .ThenInclude(x => x.Characteristics)
                    .ThenInclude(x => x.CharacteristicsValue)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<IEnumerable<ShowcaseProductDTO>>(products);
        return result;
    }
}