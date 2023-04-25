using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Categories.DTO;

public class SubcategoryDTO : IMapWith<Subcategory>
{
    public string Name { get; set; } = null!;
    public IEnumerable<SubcategoryFilterDTO> Filters { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Subcategory, SubcategoryDTO>();
    }
}