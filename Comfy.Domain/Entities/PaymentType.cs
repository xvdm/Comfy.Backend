namespace Comfy.Domain.Entities;

public sealed class PaymentType
{
    public int Id { get; set; }
    public string Type { get; set; } = null!;
}