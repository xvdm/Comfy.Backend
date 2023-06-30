using Comfy.Application.Handlers.Products.DTO;

namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsBySearchTerm.DTO;

public sealed record ProductsPageBySearchTermDTO
{
    public int TotalProductsNumber { get; set; }
    public IEnumerable<ShowcaseProductDTO> Products { get; set; } = null!;
}