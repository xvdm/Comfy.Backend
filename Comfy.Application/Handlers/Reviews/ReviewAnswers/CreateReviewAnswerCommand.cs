using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MediatR;

namespace Comfy.Application.Handlers.Reviews.ReviewAnswers;

public sealed record CreateReviewAnswerCommand : IRequest, IJwtValidation
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
        var answer = new ReviewAnswer
        {
            ReviewId = request.ReviewId,
            Text = request.Text,
            UserId = request.UserId
        };
        _context.ReviewAnswers.Add(answer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}