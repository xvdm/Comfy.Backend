namespace Comfy.Application.Handlers.TwoFactorAuthentication.DTO;

public sealed record SetupInfoDTO(string QrCodeUrl, string AuthKey);