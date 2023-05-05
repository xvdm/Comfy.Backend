﻿using AutoMapper;
using Comfy.Application.Handlers.Questions.DTO;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Questions;

public record GetQuestionsQuery(int ProductId) : IRequest<IEnumerable<QuestionDTO>>;


public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, IEnumerable<QuestionDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuestionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QuestionDTO>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = await _context.Questions
            .Include(x => x.User)
            .Include(x => x.Answers.Where(y => y.IsActive == true))
                .ThenInclude(x => x.User)
            .Where(x => x.ProductId == request.ProductId)
            .Where(x => x.IsActive == true)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<IEnumerable<QuestionDTO>>(questions);
        return result;
    }
}