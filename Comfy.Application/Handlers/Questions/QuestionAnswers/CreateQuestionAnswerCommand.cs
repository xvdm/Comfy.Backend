using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.LocalizationStrings;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var question = await _context.Questions.CountAsync(x => x.Id == request.QuestionId, cancellationToken);
        if (question <= 0) throw new NotFoundException(LocalizationStrings.Question);

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