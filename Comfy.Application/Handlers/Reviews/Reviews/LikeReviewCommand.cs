using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews.Reviews;

public record LikeReviewCommand(int ReviewId) : IRequest;


public class LikeReviewCommandHandler : IRequestHandler<LikeReviewCommand>
{
    private readonly IApplicationDbContext _context;

    public LikeReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LikeReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
        if (review is null) return;
        review.Likes += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}