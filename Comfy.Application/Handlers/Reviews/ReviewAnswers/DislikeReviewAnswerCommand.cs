using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.LocalizationStrings;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews.ReviewAnswers;

public sealed record DislikeReviewAnswerCommand(int ReviewAnswerId, Guid UserId) : IRequest, IJwtValidation;


public sealed class DislikeReviewAnswerCommandHandler : IRequestHandler<DislikeReviewAnswerCommand>
{
    private readonly IApplicationDbContext _context;

    public DislikeReviewAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DislikeReviewAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = await _context.ReviewAnswers.FirstOrDefaultAsync(x => x.Id == request.ReviewAnswerId, cancellationToken);
        if (answer is null) throw new NotFoundException(LocalizationStrings.ReviewAnswer);
        answer.Dislikes += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}