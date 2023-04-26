using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.DTO;

public record ShowcaseProductDTO : IMapWith<Product>
{
    public string Name { get; init; } = null!;
    public int Price { get; init; }
    public int DiscountAmount { get; init; }
    public int Amount { get; init; }
    public double Rating { get; init; }
    public string Url { get; init; } = null!;

    public IEnumerable<Image> Images { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ShowcaseProductDTO>();
    }
}