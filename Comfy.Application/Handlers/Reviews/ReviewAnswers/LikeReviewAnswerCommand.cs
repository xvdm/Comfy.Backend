using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.LocalizationStrings;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews.ReviewAnswers;

public record LikeReviewAnswerCommand(int ReviewAnswerId) : IRequest;


public class LikeReviewAnswerCommandHandler : IRequestHandler<LikeReviewAnswerCommand>
{
    private readonly IApplicationDbContext _context;

    public LikeReviewAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LikeReviewAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = await _context.ReviewAnswers.FirstOrDefaultAsync(x => x.Id == request.ReviewAnswerId, cancellationToken);
        if (answer is null) throw new NotFoundException(LocalizationStrings.ReviewAnswer);
        answer.Likes += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}