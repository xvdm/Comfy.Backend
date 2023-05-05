using Comfy.Domain.Models;

namespace Comfy.Domain.ViewModels;

public class ProductsViewModel
{
    public int CategoryId { get; set; }
    public string? Query { get; set; }
    public Dictionary<CharacteristicName, List<CharacteristicValue>>? Characteristics { get; set; }
    public IEnumerable<Product> Products { get; set; } = null!;
    public IEnumerable<Brand> Brands { get; set; } = null!;
}