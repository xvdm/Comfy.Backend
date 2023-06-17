using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MediatR;

namespace Comfy.Application.Handlers.Questions.QuestionAnswers;

public sealed record CreateQuestionAnswerCommand : IRequest, IJwtValidation
{
    public int QuestionId { get; init; }
    public Guid UserId { get; init; }
    public string Text { get; init; } = null!;
}


public sealed class CreateQuestionAnswerCommandHandler : IRequestHandler<CreateQuestionAnswerCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateQuestionAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateQuestionAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = new QuestionAnswer
        {
            QuestionId = request.QuestionId,
            Text = request.Text,
            UserId = request.UserId
        };
        _context.QuestionAnswers.Add(answer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}