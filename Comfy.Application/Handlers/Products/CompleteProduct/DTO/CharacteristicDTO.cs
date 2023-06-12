using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public sealed record CharacteristicDTO : IMapWith<Characteristic>
{
    public string Name { get; init; } = null!;
    public string Value { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Characteristic, CharacteristicDTO>()
            .ForMember(dto => dto.Name, x => x.MapFrom(characteristic => characteristic.CharacteristicsName.Name))
            .ForMember(dto => dto.Value, x => x.MapFrom(characteristic => characteristic.CharacteristicsValue.Value));
    }
}