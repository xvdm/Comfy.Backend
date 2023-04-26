using AutoMapper;
using Comfy.Application.Handlers.Categories.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Categories;

public record GetCategoriesMenuQuery : IRequest<CategoriesMenuDTO>;


public class GetCategoriesMenuQueryHandler : IRequestHandler<GetCategoriesMenuQuery, CategoriesMenuDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesMenuQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoriesMenuDTO> Handle(GetCategoriesMenuQuery request, CancellationToken cancellationToken)
    {
        var mainCategories = await _context.MainCategories
            .Include(x => x.Categories)
                .ThenInclude(x => x.Filters)
            .ToListAsync(cancellationToken);

        var dto = _mapper.Map<IEnumerable<MainCategoryDTO>>(mainCategories);

        var result = new CategoriesMenuDTO()
        {
            MainCategories = dto
        };
        return result;
    }
}