using Comfy.Domain.Base;
using Comfy.Domain.Identity;

namespace Comfy.Domain.Entities;

public sealed class Question : Auditable
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public bool IsActive { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public ICollection<QuestionAnswer> Answers { get; set; } = null!;
}