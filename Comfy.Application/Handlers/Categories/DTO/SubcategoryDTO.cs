using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Categories.DTO;

public sealed record SubcategoryDTO : IMapWith<Subcategory>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public IEnumerable<SubcategoryFilterDTO> Filters { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Subcategory, SubcategoryDTO>();
    }
}