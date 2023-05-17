using Comfy.Domain.Base;
using Comfy.Domain.Identity;

namespace Comfy.Domain.Models;

public sealed class ReviewAnswer : Auditable
{
    public int Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string Text { get; set; } = null!;
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public bool IsActive { get; set; }

    public int ReviewId { get; set; }
    public Review Review { get; set; } = null!;
}