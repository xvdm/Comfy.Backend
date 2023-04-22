using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public class ImageDTO : IMapWith<Image>
{
    public string Url { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Image, ImageDTO>();
    }
}