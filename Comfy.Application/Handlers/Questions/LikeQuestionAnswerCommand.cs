using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Questions;

public record LikeQuestionAnswerCommand(int QuestionAnswerId) : IRequest;


public class LikeQuestionAnswerCommandHandler : IRequestHandler<LikeQuestionAnswerCommand>
{
    private readonly IApplicationDbContext _context;

    public LikeQuestionAnswerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LikeQuestionAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = await _context.QuestionAnswers.FirstOrDefaultAsync(x => x.Id == request.QuestionAnswerId, cancellationToken);
        if (answer is null) return;
        answer.Likes += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}