namespace Comfy.Application.Handlers.Authorization.DTO;

public sealed record SignInDTO
{
    public Guid UserId { get; init; }
    public Guid RefreshToken { get; init; }
    public string AccessToken { get; init; } = null!;
}