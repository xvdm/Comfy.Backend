using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public class ProductDTO : IMapWith<Product>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Price { get; set; }
    public int DiscountAmount { get; set; }
    public int Amount { get; set; }
    public int Code { get; set; }
    public double Rating { get; set; }
    public string Url { get; set; } = null!;

    public ICollection<PriceHistoryDTO> PriceHistory { get; set; } = null!;
    public IList<ImageDTO> Images { get; set; } = null!;
    public ICollection<CharacteristicDTO> Characteristics { get; set; } = null!;
    public ICollection<QuestionDTO> Questions { get; set; } = null!;
    public ICollection<ReviewDTO> Reviews { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDTO>();
    }
}