using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Models;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.DTO;

public sealed record CharacteristicNameDTO : IMapWith<CharacteristicName>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CharacteristicName, CharacteristicNameDTO>();
    }
}