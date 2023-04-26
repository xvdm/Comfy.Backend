using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Categories.DTO;

public record MainCategoryDTO : IMapWith<MainCategory>
{
    public string Name { get; init; } = null!;
    public IEnumerable<SubcategoryDTO> Categories { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MainCategory, MainCategoryDTO>();
    }
}