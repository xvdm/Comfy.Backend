using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Questions.DTO;

public sealed record QuestionForUserDTO : IMapWith<Question>
{
    public string Text { get; init; } = null!;
    public int Likes { get; init; }
    public int Dislikes { get; init; }
    public string? CreatedAt { get; init; }
    public string ProductUrl { get; init; } = null!;
    public string ProductName { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Question, QuestionForUserDTO>()
            .ForMember(dto => dto.CreatedAt, x => x.MapFrom(question => question.CreatedAt!.Value))
            .ForMember(dto => dto.ProductUrl, x => x.MapFrom(question => question.Product.Url))
            .ForMember(dto => dto.ProductName, x => x.MapFrom(question => question.Product.Name));
    }
}