using Comfy.Application.Common.Constants;
using Comfy.Application.Common.Exceptions;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Questions.Questions;

public sealed record DislikeQuestionCommand(int QuestionId, Guid UserId) : IRequest, IJwtValidation;


public sealed class DislikeQuestionCommandHandler : IRequestHandler<DislikeQuestionCommand>
{
    private readonly IApplicationDbContext _context;

    public DislikeQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DislikeQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == request.QuestionId, cancellationToken);
        if (question is null) throw new NotFoundException(LocalizationStrings.Question);
        question.Dislikes += 1;
        await _context.SaveChangesAsync(cancellationToken);
    }
}