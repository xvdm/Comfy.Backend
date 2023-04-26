﻿using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public record ReviewDTO : IMapWith<Review>
{
    public string Text { get; init; } = null!;
    public string Advantages { get; init; } = null!;
    public string Disadvantages { get; init; } = null!;
    public double ProductRating { get; init; }
    public int UsefulReviewCount { get; init; }
    public int NeedlessReviewCount { get; init; }
    public string Username { get; init; } = null!;
    public IEnumerable<ReviewAnswerDTO> Answers { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Review, ReviewDTO>()
            .ForMember(dto => dto.Username, x => x.MapFrom(review => review.User.UserName));
    }
}