using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Questions;

public record LikeQuestionCommand(int QuestionId) : IRequest;


public class LikeQuestionCommandHandler : IRequestHandler<LikeQuestionCommand>
{
    private readonly IApplicationDbContext _context;

    public LikeQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LikeQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == request.QuestionId, cancellationToken);
        if (question is null) return;
        question.UsefulQuestionCount += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}