using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.DTO;

public sealed class CharacteristicValueDTO : IMapWith<CharacteristicValue>
{
    public int Id { get; init; }
    public string Value { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CharacteristicValue, CharacteristicValueDTO>();
    }
}