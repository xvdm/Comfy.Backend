namespace Comfy.Domain.Models;

public sealed class PriceHistory
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}