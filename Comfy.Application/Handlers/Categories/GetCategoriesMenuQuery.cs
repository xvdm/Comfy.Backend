using AutoMapper;
using Comfy.Application.Handlers.Categories.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Categories;

public sealed record GetCategoriesMenuQuery : IRequest<IEnumerable<MainCategoryDTO>>, ICacheable
{
    public string CacheKey => "categories-menu";
    public double ExpirationHours => 168;
}


public sealed class GetCategoriesMenuQueryHandler : IRequestHandler<GetCategoriesMenuQuery, IEnumerable<MainCategoryDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesMenuQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MainCategoryDTO>> Handle(GetCategoriesMenuQuery request, CancellationToken cancellationToken)
    {
        var mainCategories = await _context.MainCategories
            .Include(x => x.Categories)
                .ThenInclude(x => x.Filters)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<IEnumerable<MainCategoryDTO>>(mainCategories);
        return result;
    }
}