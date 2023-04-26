using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Categories.DTO;

public record SubcategoryDTO : IMapWith<Subcategory>
{
    public string Name { get; init; } = null!;
    public IEnumerable<SubcategoryFilterDTO> Filters { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Subcategory, SubcategoryDTO>();
    }
}