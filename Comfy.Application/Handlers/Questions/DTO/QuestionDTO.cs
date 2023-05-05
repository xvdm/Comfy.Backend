﻿using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Models;

namespace Comfy.Application.Handlers.Questions.DTO;

public record QuestionDTO : IMapWith<Question>
{
    public string Username { get; init; } = null!;
    public int ProductId { get; init; }

    public int Id { get; init; }
    public string Text { get; init; } = null!;
    public int UsefulQuestionCount { get; init; }
    public int NeedlessQuestionCount { get; init; }
    public IEnumerable<QuestionAnswerDTO> Answers { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Question, QuestionDTO>()
            .ForMember(dto => dto.Username, x => x.MapFrom(question => question.User.UserName));
    }
}