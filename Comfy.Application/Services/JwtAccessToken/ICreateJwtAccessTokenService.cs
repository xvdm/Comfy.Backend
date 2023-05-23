using Comfy.Domain.Identity;

namespace Comfy.Application.Services.JwtAccessToken;

public interface ICreateJwtAccessTokenService
{
    public Task<string> CreateToken(User user);
}