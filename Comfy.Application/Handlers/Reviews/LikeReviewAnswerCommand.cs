﻿using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews;

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
        if (answer is null) return;
        answer.UsefulAnswerCount += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}