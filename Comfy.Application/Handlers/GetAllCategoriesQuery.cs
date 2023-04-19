using Comfy.Application.Interfaces;
using Comfy.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers;

public class GetAllCategoriesQuery : IRequest<ICollection<MainCategory>>
{
}

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, ICollection<MainCategory>>
{
    private readonly IApplicationDbContext _context;

    public GetAllCategoriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<MainCategory>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.MainCategories
            .Include(x => x.Categories)
            .ToListAsync(cancellationToken);

        return result;
    }
}