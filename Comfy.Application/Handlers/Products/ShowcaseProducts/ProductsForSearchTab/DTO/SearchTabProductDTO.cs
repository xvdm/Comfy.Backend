namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsForSearchTab.DTO;

public sealed record SearchTabProductDTO
{
    public string Name { get; init; } = null!;
    public decimal Price { get; init; }
    public int DiscountAmount { get; init; }
    public string Url { get; init; } = null!;
    public string? ImageUrl { get; init; } = null!;
}