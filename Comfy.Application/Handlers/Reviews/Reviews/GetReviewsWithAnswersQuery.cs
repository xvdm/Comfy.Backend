using AutoMapper;
using Comfy.Application.Handlers.Reviews.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews.Reviews;

public sealed record GetReviewsWithAnswersQuery : IRequest<ReviewsDTO>, ICacheable
{
    public int ProductId { get; init; }

    public string CacheKey => $"product-reviews:{ProductId}:{PageNumber}:{PageSize}";
    public double ExpirationHours => 3;

    private const int MaxPageSize = 10;
    private int _pageSize = MaxPageSize;
    private int _pageNumber = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value is > MaxPageSize or < 1 ? MaxPageSize : value;
    }
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    public GetReviewsWithAnswersQuery(int productId, int? pageNumber, int? pageSize)
    {
        ProductId = productId;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
        if (pageSize is not null) PageSize = (int)pageSize;
    }
}


public sealed class GetReviewsWithAnswersQueryHandler : IRequestHandler<GetReviewsWithAnswersQuery, ReviewsDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetReviewsWithAnswersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReviewsDTO> Handle(GetReviewsWithAnswersQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .Include(x => x.User)
            .Include(x => x.Answers.Where(y => y.IsActive == true))
                .ThenInclude(x => x.User)
            .Where(x => x.ProductId == request.ProductId)
            .Where(x => x.IsActive == true)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
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