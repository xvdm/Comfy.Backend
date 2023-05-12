using Comfy.Domain.Base;
using Comfy.Domain.Identity;

namespace Comfy.Domain.Models;

public class Review : Auditable
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public string Advantages { get; set; } = null!;
    public string Disadvantages { get; set; } = null!;
    public double ProductRating { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public bool IsActive { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public ICollection<ReviewAnswer> Answers { get; set; } = null!;
}