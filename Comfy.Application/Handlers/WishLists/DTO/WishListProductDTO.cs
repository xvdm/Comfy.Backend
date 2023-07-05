using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.WishLists.DTO;

public sealed record WishListProductDTO : IMapWith<Product>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int DiscountAmount { get; set; }
    public int Code { get; set; }
    public double Rating { get; set; }
    public int ReviewsNumber { get; set; }
    public string Url { get; set; } = null!;
    public string? ImageUrl { get; set; } 

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, WishListProductDTO>()
            .ForMember(dto => dto.Rating, x => x.MapFrom(product => Math.Round(product.Rating, 1)))
            .ForMember(dto => dto.ImageUrl, x => x.MapFrom(product => product.Images.FirstOrDefault()!.Url));
    }
}