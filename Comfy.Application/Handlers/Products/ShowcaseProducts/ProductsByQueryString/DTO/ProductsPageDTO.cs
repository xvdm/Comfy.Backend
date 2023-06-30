using Comfy.Application.Handlers.Products.DTO;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.DTO;

public sealed record ProductsPageDTO
{
    public int TotalProductsNumber { get; set; }
    public int SubcategoryId { get; set; }
    public string? QueryString { get; set; }
    public IEnumerable<CharacteristicDTO> Characteristics { get; set; } = null!;
    public IEnumerable<BrandDTO> Brands { get; set; } = null!;
    public IEnumerable<ShowcaseProductDTO> Products { get; set; } = null!;
}