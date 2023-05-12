using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Questions;

public record DislikeQuestionAnswerCommand(int QuestionAnswerId) : IRequest;


public class DislikeQuestionAnswerCommandHandler : IRequestHandler<DislikeQuestionAnswerCommand>
{
    private readonly IApplicationDbContext _context;

    public DislikeQuestionAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DislikeQuestionAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = await _context.QuestionAnswers.FirstOrDefaultAsync(x => x.Id == request.QuestionAnswerId, cancellationToken);
        if (answer is null) return;
        answer.Dislikes += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}