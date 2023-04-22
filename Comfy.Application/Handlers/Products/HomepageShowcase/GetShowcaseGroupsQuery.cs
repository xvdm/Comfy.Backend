using AutoMapper;
using Comfy.Application.Handlers.Products.HomepageShowcase.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Products.HomepageShowcase;

public class GetShowcaseGroupsQuery : IRequest<ICollection<ShowcaseGroupDTO>>
{
}

public class GetShowcaseGroupsQueryHandler : IRequestHandler<GetShowcaseGroupsQuery, ICollection<ShowcaseGroupDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShowcaseGroupsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ShowcaseGroupDTO>> Handle(GetShowcaseGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await _context.ShowcaseGroups
            .Include(x => x.Products)
                .ThenInclude(x => x.Images.Take(3))
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<ShowcaseGroupDTO>>(groups);

        return result;
    }
}