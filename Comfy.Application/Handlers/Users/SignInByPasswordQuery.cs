﻿using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.Helpers;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Comfy.Application.Handlers.Users;

public sealed record SignInByPasswordQuery : IRequest<string>
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}

public sealed class SignInByPasswordQueryHandler : IRequestHandler<SignInByPasswordQuery, string>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public SignInByPasswordQueryHandler(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> Handle(SignInByPasswordQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is null) throw new BadCredentialsException();
        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (isValidPassword == false) throw new BadCredentialsException();

        var userClaims = await _userManager.GetClaimsAsync(user);
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));

        var token = new JwtSecurityTokenHandler().WriteToken(JwtHelper.CreateToken(_configuration, userClaims));

        return token;
    }
}