using Comfy.Domain.Identity;

namespace Comfy.Domain.Entities;

public sealed class RefreshToken
{
    public int Id { get; set; }
    public Guid Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool Invalidated { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}