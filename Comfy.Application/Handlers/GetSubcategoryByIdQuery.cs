using Comfy.Application.Interfaces;
using Comfy.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers;

public class GetSubcategoryByIdQuery : IRequest<Subcategory?>
{
    public int CategoryId { get; set; }
    public GetSubcategoryByIdQuery(int categoryId)
    {
        CategoryId = categoryId;
    }
}

public class GetSubcategoryByIdQueryHandler : IRequestHandler<GetSubcategoryByIdQuery, Subcategory?>
{
    private readonly IApplicationDbContext _context;

    public GetSubcategoryByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Subcategory?> Handle(GetSubcategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Subcategories
            .Where(x => x.Id == request.CategoryId)
            .Include(x => x.UniqueCharacteristics)
                .ThenInclude(x => x.CharacteristicsName)
            .Include(x => x.UniqueCharacteristics)
                .ThenInclude(x => x.CharacteristicsValue)
            .FirstOrDefaultAsync(cancellationToken);

        return category;
    }
}