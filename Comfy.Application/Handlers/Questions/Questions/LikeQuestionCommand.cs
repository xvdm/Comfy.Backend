using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.LocalizationStrings;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Questions.Questions;

public sealed record LikeQuestionCommand(int QuestionId) : IRequest;


public sealed class LikeQuestionCommandHandler : IRequestHandler<LikeQuestionCommand>
{
    private readonly IApplicationDbContext _context;

    public LikeQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LikeQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == request.QuestionId, cancellationToken);
        if (question is null) throw new NotFoundException(LocalizationStrings.Question);
        question.Likes += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}