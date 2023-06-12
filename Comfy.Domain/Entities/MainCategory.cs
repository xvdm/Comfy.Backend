namespace Comfy.Domain.Entities;

public sealed class MainCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int? ImageId { get; set; }
    public MainCategoryImage? Image { get; set; } = null!;

    public ICollection<Subcategory> Categories { get; set; } = null!;
}