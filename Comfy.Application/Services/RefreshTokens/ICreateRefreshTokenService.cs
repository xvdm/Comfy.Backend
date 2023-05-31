namespace Comfy.Application.Services.RefreshTokens;

public interface ICreateRefreshTokenService
{
    public Task<Guid> CreateToken(Guid userId, CancellationToken cancellationToken);
}