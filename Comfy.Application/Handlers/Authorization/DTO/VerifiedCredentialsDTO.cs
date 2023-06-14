namespace Comfy.Application.Handlers.Authorization.DTO;

public record VerifiedCredentialsDTO
{
    public bool ValidCredentials { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public Guid UserId { get; set; }
}