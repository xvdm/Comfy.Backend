namespace Comfy.Domain.Entities;

public sealed class OrderStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = null!;
}

public enum OrderStatusEnum
{
    Pending = 1,
    Processing = 2,
    Shipped = 3,
    InTransit = 4,
    OutForDelivery = 5,
    Delivered = 6,
    Cancelled = 7,
    Returned = 8
}