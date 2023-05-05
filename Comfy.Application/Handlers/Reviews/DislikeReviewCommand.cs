using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews;

public record DislikeReviewCommand(int ReviewId) : IRequest;


public class DislikeReviewCommandHandler : IRequestHandler<DislikeReviewCommand>
{
    private readonly IApplicationDbContext _context;

    public DislikeReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DislikeReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
        if (review is null) return;
        review.NeedlessReviewCount += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}