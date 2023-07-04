using Comfy.Domain.Base;
using Comfy.Domain.Identity;

namespace Comfy.Domain.Entities;

public sealed class Order : Auditable
{
    public int Id { get; set; }

    public int OrderStatusId { get; set; }
    public OrderStatus OrderStatus { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Patronymic { get; set; } = null!;

    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;

    public string? UserComment { get; set; }
    public bool CallToConfirm { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = null!;
}