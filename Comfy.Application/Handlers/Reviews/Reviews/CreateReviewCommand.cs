﻿using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MediatR;

namespace Comfy.Application.Handlers.Reviews.Reviews;

public sealed record CreateReviewCommand : IRequest, IJwtValidation
{
    public int ProductId { get; init; }
    public string Text { get; init; } = null!;
    public string Advantages { get; init; } = null!;
    public string Disadvantages { get; init; } = null!;
    public double ProductRating { get; init; }
    public Guid UserId { get; init; }
}


public sealed class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            ProductId = request.ProductId,
            Text = request.Text,
            Advantages = request.Advantages,
            Disadvantages = request.Disadvantages,
            ProductRating = request.ProductRating,
            UserId = request.UserId,
            IsActive = false
        };
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);
    }
}