using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Questions.DTO;

public sealed record QuestionAnswerDTO : IMapWith<QuestionAnswer>
{
    public string Username { get; init; } = null!;

    public int Id { get; init; }
    public string Text { get; init; } = null!;
    public int Likes { get; init; }
    public int Dislikes { get; init; }
    public string? CreatedAt { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<QuestionAnswer, QuestionAnswerDTO>()
            .ForMember(dto => dto.CreatedAt, x => x.MapFrom(answer => answer.CreatedAt!.Value))
            .ForMember(dto => dto.Username, x => x.MapFrom(answer => answer.User.Name));
    }
}