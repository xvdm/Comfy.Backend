namespace Comfy.Domain.Entities;

public sealed class Subcategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? ImageUrl { get; set; }

    public int MainCategoryId { get; set; }
    public MainCategory MainCategory { get; set; } = null!;

    public ISet<CategoryUniqueCharacteristic> UniqueCharacteristics { get; set; } = null!;
    public ISet<Brand> UniqueBrands { get; set; } = null!;
    public ICollection<SubcategoryFilter> Filters { get; set; } = null!;
}