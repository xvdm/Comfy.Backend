using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Categories.DTO;

public sealed record MainCategoryDTO : IMapWith<MainCategory>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public IEnumerable<SubcategoryDTO> Categories { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MainCategory, MainCategoryDTO>();
    }
}