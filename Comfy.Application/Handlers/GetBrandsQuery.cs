using Comfy.Application.Interfaces;
using Comfy.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comfy.Application.Handlers
{
    public class GetBrandsQuery : IRequest<IEnumerable<Brand>>
    {
        public int CategoryId { get; set; }
        public GetBrandsQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }

    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, IEnumerable<Brand>>
    {
        private readonly IComfyDbContext _context;

        public GetBrandsQueryHandler(IComfyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Subcategories
                .Include(x => x.UniqueBrands)
                .FirstOrDefaultAsync(x => x.Id == request.CategoryId);

            if (category is null) throw new HttpRequestException("There is no category with given id");

            var brands = new List<Brand>();
            if (category.UniqueBrands is not null)
            {
                brands = category.UniqueBrands.ToList();
            }

            return brands;
        }
    }
}
