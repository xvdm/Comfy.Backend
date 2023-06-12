namespace Comfy.Application.Common.Configuration;

public sealed record EmailConfiguration
{
    public string DisplayName { get; set; } = null!;
    public string From { get; set; } = null!;
    public string Host { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Port { get; set; }
}