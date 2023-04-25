using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Banners.DTO;

public class BannerDTO : IMapWith<Banner>
{
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string PageUrl { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Banner, BannerDTO>();
    }
}