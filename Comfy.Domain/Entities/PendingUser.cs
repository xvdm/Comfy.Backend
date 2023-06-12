namespace Comfy.Domain.Entities;

public class PendingUser
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string ConfirmationCode { get; set; } = null!;
    public int AccessFailedCount { get; set; }
    public DateTime? LockoutEnd { get; set; }
}