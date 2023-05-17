﻿using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.LocalizationStrings;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Reviews.ReviewAnswers;

public sealed record CreateReviewAnswerCommand : IRequest
{
    public int ReviewId { get; init; }
    public Guid UserId { get; init; }
    public string Text { get; init; } = null!;
}


public sealed class CreateReviewAnswerCommandHandler : IRequestHandler<CreateReviewAnswerCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateReviewAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateReviewAnswerCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.CountAsync(x => x.Id == request.ReviewId, cancellationToken);
        if (review <= 0) throw new NotFoundException(LocalizationStrings.Review);

        var answer = new ReviewAnswer()
        {
            ReviewId = request.ReviewId,
            Text = request.Text,
            UserId = request.UserId
        };
        _context.ReviewAnswers.Add(answer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}