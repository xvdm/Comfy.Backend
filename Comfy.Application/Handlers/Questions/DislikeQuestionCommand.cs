using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Questions;

public record DislikeQuestionCommand(int QuestionId) : IRequest;


public class DislikeQuestionCommandHandler : IRequestHandler<DislikeQuestionCommand>
{
    private readonly IApplicationDbContext _context;

    public DislikeQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DislikeQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == request.QuestionId, cancellationToken);
        if (question is null) return;
        question.Dislikes += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}