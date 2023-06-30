using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsForSearchTab.DTO;

public sealed record SearchTabProductDTO : IMapWith<Product>
{
    public string Name { get; init; } = null!;
    public decimal Price { get; init; }
    public int DiscountAmount { get; init; }
    public string Url { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, SearchTabProductDTO>();
    }
}