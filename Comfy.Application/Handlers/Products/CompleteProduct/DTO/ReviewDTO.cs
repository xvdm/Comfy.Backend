using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public class ReviewDTO : IMapWith<Review>
{
    public string Text { get; set; } = null!;
    public string Advantages { get; set; } = null!;
    public string Disadvantages { get; set; } = null!;
    public double ProductRating { get; set; }
    public int UsefulReviewCount { get; set; }
    public int NeedlessReviewCount { get; set; }

    public string Username { get; set; } = null!;

    public ICollection<ReviewAnswerDTO> Answers { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Review, ReviewDTO>()
            .ForMember(dto => dto.Username, x => x.MapFrom(review => review.User.UserName));
    }
}