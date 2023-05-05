using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Questions;

public record CreateQuestionAnswerCommand : IRequest
{
    public int QuestionId { get; init; }
    public Guid UserId { get; init; }
    public string Text { get; init; } = null!;
}


public class CreateQuestionAnswerCommandHandler : IRequestHandler<CreateQuestionAnswerCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateQuestionAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateQuestionAnswerCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.CountAsync(x => x.Id == request.QuestionId, cancellationToken);
        if (question <= 0) return;

        var answer = new QuestionAnswer()
        {
            QuestionId = request.QuestionId,
            Text = request.Text,
            UserId = request.UserId
        };
        await _context.QuestionAnswers.AddAsync(answer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}