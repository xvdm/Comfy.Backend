using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.DTO;

public sealed record BrandDTO : IMapWith<Brand>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Brand, BrandDTO>();
    }
}