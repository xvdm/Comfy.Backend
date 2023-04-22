using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public class ReviewAnswerDTO : IMapWith<ReviewAnswer>
{
    public string Username { get; set; } = null!;
    public string Text { get; set; } = null!;
    public int UsefulAnswerCount { get; set; }
    public int NeedlessAnswerCount { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ReviewAnswer, ReviewAnswerDTO>()
            .ForMember(dto => dto.Username, x => x.MapFrom(answer => answer.User.UserName));
    }
}