using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.DTO;

public class ShowcaseProductDTO : IMapWith<Product>
{
    public string Name { get; set; } = null!;
    public int Price { get; set; }
    public int DiscountAmount { get; set; }
    public int Amount { get; set; }
    public double Rating { get; set; }
    public string Url { get; set; } = null!;

    public IEnumerable<Image> Images { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ShowcaseProductDTO>();
    }
}