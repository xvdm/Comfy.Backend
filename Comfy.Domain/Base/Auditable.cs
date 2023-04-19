namespace Comfy.Domain.Base;

public abstract class Auditable
{
    public DateTime? CreatedAt { get; set; }
}