using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Orders.DTO;

public sealed record OrderProductDTO : IMapWith<Product>
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Code { get; set; }
    public string Url { get; set; } = null!;
    public string? ImageUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, OrderProductDTO>()
            .ForMember(dto => dto.ImageUrl, x => x.MapFrom(product => product.Images.FirstOrDefault()!.Url));
    }
}