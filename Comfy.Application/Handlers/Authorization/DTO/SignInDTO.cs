namespace Comfy.Application.Handlers.Authorization.DTO;

public sealed record SignInDTO
{
    public Guid UserId { get; init; }
    public string AccessToken { get; init; } = null!;
    public Guid RefreshToken { get; init; }
}