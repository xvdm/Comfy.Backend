using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MediatR;

namespace Comfy.Application.Handlers.Questions.Questions;

public sealed record CreateQuestionCommand : IRequest, IJwtValidation
{
    public int ProductId { get; init; }
    public string Text { get; init; } = null!;
    public Guid UserId { get; init; }
}


public sealed class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = new Question
        {
            ProductId = request.ProductId,
            Text = request.Text,
            UserId = request.UserId,
            IsActive = false
        };
        _context.Questions.Add(question);
        await _context.SaveChangesAsync(cancellationToken);
    }
}