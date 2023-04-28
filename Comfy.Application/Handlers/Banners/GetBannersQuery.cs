using AutoMapper;
using Comfy.Application.Handlers.Banners.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Banners;

public record GetBannersQuery : IRequest<IEnumerable<BannerDTO>>;


public class GetBannersQueryHandler : IRequestHandler<GetBannersQuery, IEnumerable<BannerDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;


    public GetBannersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BannerDTO>> Handle(GetBannersQuery request, CancellationToken cancellationToken)
    {
        var banners = await _context.Banners
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<IEnumerable<BannerDTO>>(banners);
        return result;
    }
}