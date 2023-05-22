using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.Helpers;
using Comfy.Application.Handlers.Authorization.DTO;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Comfy.Application.Handlers.Authorization;

public sealed record SignInByPasswordQuery : IRequest<SignInDTO>
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}

public sealed class SignInByPasswordQueryHandler : IRequestHandler<SignInByPasswordQuery, SignInDTO>
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public SignInByPasswordQueryHandler(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<SignInDTO> Handle(SignInByPasswordQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is null) throw new BadCredentialsException();
        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (isValidPassword == false) throw new BadCredentialsException();

        var userClaims = await _userManager.GetClaimsAsync(user);
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityTokenHandler().WriteToken(JwtHelper.CreateToken(_configuration, userClaims));


        var result = new SignInDTO
        {
            UserId = user.Id,
            AccessToken = token
        };
        return result;
    }
}