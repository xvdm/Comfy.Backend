using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Models;

namespace Comfy.Application.Handlers.Reviews.DTO;

public record ReviewDTO : IMapWith<Review>
{
    public string Username { get; init; } = null!;

    public int Id { get; init; }
    public string Text { get; init; } = null!;
    public string Advantages { get; init; } = null!;
    public string Disadvantages { get; init; } = null!;
    public double ProductRating { get; init; }
    public int UsefulReviewCount { get; init; }
    public int NeedlessReviewCount { get; init; }
    public IEnumerable<ReviewAnswerDTO> Answers { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Review, ReviewDTO>()
            .ForMember(dto => dto.Username, x => x.MapFrom(review => review.User.UserName));
    }
}