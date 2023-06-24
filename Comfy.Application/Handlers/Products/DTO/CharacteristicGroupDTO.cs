using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Application.Handlers.Products.CompleteProduct.DTO;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Products.DTO;

public class CharacteristicGroupDTO : IMapWith<CharacteristicGroup>
{
    public string Name { get; set; } = null!;

    public IEnumerable<CharacteristicDTO> Characteristics { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CharacteristicGroup, CharacteristicGroupDTO>();
    }
}