using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Comfy.Application.Common.Exceptions;
using Comfy.Application.Interfaces;
using Comfy.Application.Services.JwtAccessToken;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Comfy.Application.Handlers.Authorization;

public sealed record RefreshAccessTokenCommand(Guid UserId, string AccessToken, Guid RefreshToken) : IRequest<string>;


public sealed class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly ICreateJwtAccessTokenService _createJwtAccessTokenService;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public RefreshAccessTokenCommandHandler(IApplicationDbContext context, UserManager<User> userManager, ICreateJwtAccessTokenService createJwtAccessTokenService, TokenValidationParameters tokenValidationParameters)
    {
        _context = context;
        _userManager = userManager;
        _createJwtAccessTokenService = createJwtAccessTokenService;
        _tokenValidationParameters = tokenValidationParameters;
        _tokenValidationParameters.ValidateLifetime = false;
    }

    public async Task<string> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
        if (refreshToken is null || refreshToken.Invalidated || refreshToken.UserId != request.UserId || refreshToken.ExpirationDate < DateTime.UtcNow) 
            throw new UnauthorizedException();

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        var validatedToken = GetPrincipalFromToken(request.AccessToken);
        var sub = validatedToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        var sid = validatedToken.Claims.First(x => x.Type == ClaimTypes.Sid).Value;

        if (sub != user.Id.ToString() || sid != user.SecurityStamp) throw new UnauthorizedException();

        var accessToken = await _createJwtAccessTokenService.CreateToken(user);

        return accessToken;
    }

    private ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
            _tokenValidationParameters.ValidateLifetime = true;
            if (IsJwtWithValidSecurityAlgorithm(validatedToken) == false) return null!;
            return principal;
        }
        catch
        {
            return null!;
        }
    }

    private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return validatedToken is JwtSecurityToken jwtSecurityToken &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
    }
}