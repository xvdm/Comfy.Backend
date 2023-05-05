using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Reviews.DTO;

public record ReviewAnswerDTO : IMapWith<ReviewAnswer>
{
    public string Username { get; init; } = null!;

    public string Text { get; init; } = null!;
    public int UsefulAnswerCount { get; init; }
    public int NeedlessAnswerCount { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ReviewAnswer, ReviewAnswerDTO>()
            .ForMember(dto => dto.Username, x => x.MapFrom(answer => answer.User.UserName));
    }
}