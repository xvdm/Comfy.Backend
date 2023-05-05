using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews;

public record DislikeReviewAnswerCommand(int ReviewAnswerId) : IRequest;


public class DislikeReviewAnswerCommandHandler : IRequestHandler<DislikeReviewAnswerCommand>
{
    private readonly IApplicationDbContext _context;

    public DislikeReviewAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DislikeReviewAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = await _context.ReviewAnswers.FirstOrDefaultAsync(x => x.Id == request.ReviewAnswerId, cancellationToken);
        if (answer is null) return;
        answer.NeedlessAnswerCount += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}