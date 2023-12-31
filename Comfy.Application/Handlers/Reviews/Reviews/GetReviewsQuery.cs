﻿using AutoMapper;
using Comfy.Application.Handlers.Reviews.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews.Reviews;

public sealed record GetReviewsQuery : IRequest<ReviewsDTO>
{
    public int ProductId { get; init; }

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

    public GetReviewsQuery(int productId, int? pageNumber, int? pageSize)
    {
        ProductId = productId;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
        if (pageSize is not null) PageSize = (int)pageSize;
    }
}


public sealed class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, ReviewsDTO>
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
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var mappedReviews = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);

        var totalReviewsNumber = await _context.Questions.CountAsync(x => x.IsActive && x.ProductId == request.ProductId, cancellationToken);

        var result = new ReviewsDTO
        {
            ProductId = request.ProductId,
            Reviews = mappedReviews,
            TotalReviewsNumber = totalReviewsNumber
        };
        return result;
    }
}