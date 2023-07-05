using Comfy.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Domain.Identity;

public sealed class User : IdentityUser<Guid>
{
    public string Name { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = null!;
    public WishList? WishList { get; set; }
}