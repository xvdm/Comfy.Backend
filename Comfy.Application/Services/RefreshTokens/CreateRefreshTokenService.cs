using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Comfy.Application.Services.RefreshTokens;

public sealed class CreateRefreshTokenService : ICreateRefreshTokenService
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public CreateRefreshTokenService(IApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<Guid> CreateToken(Guid userId, CancellationToken cancellationToken)
    {
        var newRefreshToken = new RefreshToken
        {
            ExpirationDate = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["RefreshToken:Lifetime"])),
            Invalidated = false,
            Token = Guid.NewGuid(),
            UserId = userId
        };
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        if (refreshToken is null) _context.RefreshTokens.Add(newRefreshToken);
        else
        {
            refreshToken.ExpirationDate = newRefreshToken.ExpirationDate;
            refreshToken.Token = newRefreshToken.Token;
        }
        await _context.SaveChangesAsync(cancellationToken);

        return newRefreshToken.Token;
    }
}