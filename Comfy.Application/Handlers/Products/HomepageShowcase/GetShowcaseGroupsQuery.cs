﻿using AutoMapper;
using Comfy.Application.Handlers.Products.HomepageShowcase.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Products.HomepageShowcase;

public sealed record GetShowcaseGroupsQuery : IRequest<IEnumerable<ShowcaseGroupDTO>>, ICacheable
{
    public string CacheKey => "showcase-groups";
    public double ExpirationHours => 168;
}


public sealed class GetShowcaseGroupsQueryHandler : IRequestHandler<GetShowcaseGroupsQuery, IEnumerable<ShowcaseGroupDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShowcaseGroupsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShowcaseGroupDTO>> Handle(GetShowcaseGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await _context.ShowcaseGroups
            .Include(x => x.Products)
                .ThenInclude(x => x.Images.OrderBy(y => y.Id).Take(3))
            .Include(x => x.Products)
                .ThenInclude(x => x.CharacteristicGroups.OrderBy(y => y.Id).Take(1))
                    .ThenInclude(x => x.Characteristics.Take(5))
                        .ThenInclude(x => x.CharacteristicsName)
            .Include(x => x.Products)
                .ThenInclude(x => x.CharacteristicGroups)
                    .ThenInclude(x => x.Characteristics)
                        .ThenInclude(x => x.CharacteristicsValue)
            .Include(x => x.Products)
                .ThenInclude(x => x.Category)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<IEnumerable<ShowcaseGroupDTO>>(groups);

        return result;
    }
}