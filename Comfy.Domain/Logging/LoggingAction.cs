namespace Comfy.Domain.Logging;

public sealed class LoggingAction
{
    public int Id { get; set; }

    public string Action { get; set; } = null!;
}