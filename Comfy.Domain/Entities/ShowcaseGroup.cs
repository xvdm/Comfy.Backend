namespace Comfy.Domain.Entities;

public sealed class ShowcaseGroup
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? QueryString { get; set; }
    public int SubcategoryId { get; set; }
    public Subcategory Subcategory { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = null!;
}