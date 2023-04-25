using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Categories.DTO;

public class MainCategoryDTO : IMapWith<MainCategory>
{
    public string Name { get; set; } = null!;
    public IEnumerable<SubcategoryDTO> Categories { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MainCategory, MainCategoryDTO>();
    }
}