using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Categories.DTO;

public record SubcategoryFilterDTO : IMapWith<SubcategoryFilter>
{
    public string Name { get; set; } = null!;
    public string FilterQuery { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SubcategoryFilter, SubcategoryFilterDTO>();
    }
}