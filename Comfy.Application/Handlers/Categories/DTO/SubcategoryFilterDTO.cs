using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Models;

namespace Comfy.Application.Handlers.Categories.DTO;

public sealed record SubcategoryFilterDTO : IMapWith<SubcategoryFilter>
{
    public string Name { get; init; } = null!;
    public string FilterQuery { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SubcategoryFilter, SubcategoryFilterDTO>();
    }
}