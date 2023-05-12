using Comfy.Domain.Base;
using Comfy.Domain.Identity;

namespace Comfy.Domain.Models;

public class Order : Auditable
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public decimal TotalSum { get; set; }
    public DateTime ReceivingDate { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public int AddressId { get; set; }
    public Address Address { get; set; } = null!;

    public int PaymentTypeId { get; set; }
    public PaymentType PaymentType { get; set; } = null!;

    public int StatusId { get; set; }
    public OrderStatus Status { get; set; } = null!;

    public ICollection<Product> OrderedProducts { get; set; } = null!;
}