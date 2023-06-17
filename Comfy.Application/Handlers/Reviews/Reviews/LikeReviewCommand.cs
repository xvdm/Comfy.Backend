using Comfy.Application.Common.Constants;
using Comfy.Application.Common.Exceptions;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews.Reviews;

public sealed record LikeReviewCommand(int ReviewId, Guid UserId) : IRequest, IJwtValidation;


public sealed class LikeReviewCommandHandler : IRequestHandler<LikeReviewCommand>
{
    private readonly IApplicationDbContext _context;

    public LikeReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LikeReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
        if (review is null) throw new NotFoundException(LocalizationStrings.Review);
        review.Likes += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}