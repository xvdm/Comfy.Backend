using Comfy.Domain.Identity;

namespace Comfy.Domain.Entities;

public sealed class Address
{
    public int Id { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Building { get; set; } = null!;
    public int ApartmentsNumber { get; set; }
    public int PostalCode { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public int AddressTypeId { get; set; }
    public AddressType AddressType { get; set; } = null!;
}