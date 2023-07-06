using AutoMapper;
using Comfy.Application.Common.Constants;
using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Questions.DTO;
using Comfy.Application.Handlers.Reviews.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews.Reviews;

public sealed record GetUserReviewsQuery : IRequest<ReviewsForUserDTO>
{
    public Guid UserId { get; init; }

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

    public GetUserReviewsQuery(Guid userId, int? pageNumber, int? pageSize)
    {
        UserId = userId;
        if (pageNumber is not null) PageNumber = (int)pageNumber;
        if (pageSize is not null) PageSize = (int)pageSize;
    }
}


public sealed class GetUserReviewsQueryHandler : IRequestHandler<GetUserReviewsQuery, ReviewsForUserDTO>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetUserReviewsQueryHandler(UserManager<User> userManager, IMapper mapper, IApplicationDbContext context)
    {
        _userManager = userManager;
        _mapper = mapper;
        _context = context;
    }

    public async Task<ReviewsForUserDTO> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(x => x.Reviews
                .Where(y => y.IsActive)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize))
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null) throw new NotFoundException(LocalizationStrings.User);

        var mappedReviews = _mapper.Map<IEnumerable<ReviewDTO>>(user.Reviews);

        var totalReviewsNumber = await _context.Reviews.CountAsync(x => x.IsActive && x.UserId == request.UserId, cancellationToken);

        var result = new ReviewsForUserDTO
        {
            UserId = request.UserId,
            Reviews = mappedReviews,
            TotalReviewsNumber = totalReviewsNumber
        };
        return result;
    }
}