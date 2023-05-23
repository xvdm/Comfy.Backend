using Comfy.Application.Common.Helpers;
using Comfy.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace Comfy.Application.Services.JwtAccessToken;

public sealed class CreateJwtAccessTokenService : ICreateJwtAccessTokenService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public CreateJwtAccessTokenService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> CreateToken(User user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        userClaims.Add(new Claim(ClaimTypes.Sid, user.SecurityStamp));

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityTokenHandler().WriteToken(JwtHelper.CreateToken(_configuration, userClaims));
        return token;
    }
}