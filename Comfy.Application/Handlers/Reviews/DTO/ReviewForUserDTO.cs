using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Reviews.DTO;

public sealed record ReviewForUserDTO : IMapWith<Review>
{
    public string Text { get; init; } = null!;
    public string Advantages { get; init; } = null!;
    public string Disadvantages { get; init; } = null!;
    public double ProductRating { get; init; }
    public int Likes { get; init; }
    public int Dislikes { get; init; }
    public string? CreatedAt { get; init; }
    public string ProductUrl { get; init; } = null!;
    public string ProductName { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Review, ReviewForUserDTO>()
            .ForMember(dto => dto.CreatedAt, x => x.MapFrom(review => review.CreatedAt!.Value))
            .ForMember(dto => dto.ProductUrl, x => x.MapFrom(question => question.Product.Url))
            .ForMember(dto => dto.ProductName, x => x.MapFrom(question => question.Product.Name));
    }
}