using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Models;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public sealed record ImageDTO : IMapWith<Image>
{
    public string Url { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Image, ImageDTO>();
    }
}