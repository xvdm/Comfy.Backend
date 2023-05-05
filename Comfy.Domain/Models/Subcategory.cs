namespace Comfy.Domain.Models;

public class Subcategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int MainCategoryId { get; set; }
    public MainCategory MainCategory { get; set; } = null!;

    public int? ImageId { get; set; }
    public SubcategoryImage? Image { get; set; } = null!;

    public ISet<Characteristic> UniqueCharacteristics { get; set; } = null!;
    public ISet<Brand> UniqueBrands { get; set; } = null!;
    public ICollection<SubcategoryFilter> Filters { get; set; } = null!;
}