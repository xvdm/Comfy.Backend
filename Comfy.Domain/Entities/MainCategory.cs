namespace Comfy.Domain.Entities;

public sealed class MainCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? ImageUrl { get; set; }

    public ICollection<Subcategory> Categories { get; set; } = null!;
}