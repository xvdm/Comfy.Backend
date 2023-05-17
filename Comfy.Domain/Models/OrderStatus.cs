namespace Comfy.Domain.Models;

public sealed class OrderStatus
{
    public int Id { get; set; }
    public string Status { get; set; } = null!;
}