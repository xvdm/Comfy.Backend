using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Application.Handlers.Products.DTO;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.HomepageShowcase.DTO;

public record ShowcaseGroupDTO : IMapWith<ShowcaseGroup>
{
    public string Name { get; init; } = null!;
    public string QueryString { get; init; } = null!;
    public IEnumerable<ShowcaseProductDTO> Products { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ShowcaseGroup, ShowcaseGroupDTO>();
    }
}