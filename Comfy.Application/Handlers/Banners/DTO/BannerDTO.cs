using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Banners.DTO;

public record BannerDTO : IMapWith<Banner>
{
    public string Name { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
    public string PageUrl { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Banner, BannerDTO>();
    }
}