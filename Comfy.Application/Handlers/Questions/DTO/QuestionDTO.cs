﻿using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Questions.DTO;

public sealed record QuestionDTO : IMapWith<Question>
{
    public string Username { get; init; } = null!;

    public int Id { get; init; }
    public string Text { get; init; } = null!;
    public int Likes { get; init; }
    public int Dislikes { get; init; }
    public string? CreatedAt { get; init; }
    public IEnumerable<QuestionAnswerDTO> Answers { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Question, QuestionDTO>()
            .ForMember(dto => dto.CreatedAt, x => x.MapFrom(question => question.CreatedAt!.Value))
            .ForMember(dto => dto.Username, x => x.MapFrom(question => question.User.Name));
    }
}