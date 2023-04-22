using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Application.Handlers.Products.DTO;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.HomepageShowcase.DTO;

public class ShowcaseGroupDTO : IMapWith<ShowcaseGroup>
{
    public string Name { get; set; } = null!;
    public string QueryString { get; set; } = null!;
    public ICollection<ShowcaseProductDTO> Products { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ShowcaseGroup, ShowcaseGroupDTO>();
    }
}