using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public sealed record ProductDTO : IMapWith<Product>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public decimal Price { get; init; }
    public int DiscountAmount { get; init; }
    public int Amount { get; init; }
    public int Code { get; init; }
    public double Rating { get; init; }
    public int ReviewsNumber { get; set; }
    public string Url { get; init; } = null!;
    public string SubcategoryName { get; init; } = null!;
    public string MainCategoryName { get; init; } = null!;

    public IEnumerable<PriceHistoryDTO> PriceHistory { get; init; } = null!;
    public IEnumerable<ImageDTO> Images { get; init; } = null!;
    public IEnumerable<CharacteristicDTO> Characteristics { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDTO>()
            .ForMember(dto => dto.SubcategoryName, x => x.MapFrom(product => product.Category.Name))
            .ForMember(dto => dto.MainCategoryName, x => x.MapFrom(product => product.Category.MainCategory.Name));
    }
}