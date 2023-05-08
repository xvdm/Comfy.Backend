using AutoMapper;
using Comfy.Application.Handlers.Reviews.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews;

public record GetReviewsQuery(int ProductId) : IRequest<ReviewsDTO>, ICacheable
{
    public string CacheKey => $"product-reviews:{ProductId}";
    public double ExpirationHours => 3;
}


public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, ReviewsDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetReviewsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReviewsDTO> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .Include(x => x.User)
            .Include(x => x.Answers.Where(y => y.IsActive == true))
                .ThenInclude(x => x.User)
            .Where(x => x.ProductId == request.ProductId)
            .Where(x => x.IsActive == true)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var mappedReviews = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);

        var result = new ReviewsDTO
        {
            ProductId = request.ProductId,
            Reviews = mappedReviews
        };
        return result;
    }
}