using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Reviews.DTO;

public sealed record ReviewAnswerDTO : IMapWith<ReviewAnswer>
{
    public string Username { get; init; } = null!;

    public int Id { get; init; }
    public string Text { get; init; } = null!;
    public int Likes { get; init; }
    public int Dislikes { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ReviewAnswer, ReviewAnswerDTO>()
            .ForMember(dto => dto.Username, x => x.MapFrom(answer => answer.User.Name));
    }
}