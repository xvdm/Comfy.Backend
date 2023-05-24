using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Authorization.DTO;
using Comfy.Application.Interfaces;
using Comfy.Application.Services.JwtAccessToken;
using Comfy.Domain.Identity;
using Comfy.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Comfy.Application.Handlers.Authorization;

public sealed record SignInByPasswordQuery(string Username, string Password) : IRequest<SignInDTO>;


public sealed class SignInByPasswordQueryHandler : IRequestHandler<SignInByPasswordQuery, SignInDTO>
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly ICreateJwtAccessTokenService _createJwtAccessTokenService;
    private readonly IConfiguration _configuration;

    public SignInByPasswordQueryHandler(UserManager<User> userManager, IApplicationDbContext context, ICreateJwtAccessTokenService createJwtAccessTokenService, IConfiguration configuration)
    {
        _userManager = userManager;
        _context = context;
        _createJwtAccessTokenService = createJwtAccessTokenService;
        _configuration = configuration;
    }

    public async Task<SignInDTO> Handle(SignInByPasswordQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is null) throw new BadCredentialsException();
        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (isValidPassword == false) throw new BadCredentialsException();

        var accessToken = await _createJwtAccessTokenService.CreateToken(user);

        var newRefreshToken = new RefreshToken
        {
            ExpirationDate = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["RefreshToken:Lifetime"])),
            Invalidated = false,
            Token = Guid.NewGuid(),
            UserId = user.Id
        };
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
        if (refreshToken is null) _context.RefreshTokens.Add(newRefreshToken);
        else
        {
            refreshToken.ExpirationDate = newRefreshToken.ExpirationDate;
            refreshToken.Token = newRefreshToken.Token;
        }

        await _context.SaveChangesAsync(cancellationToken);

        var result = new SignInDTO
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = newRefreshToken.Token
        };
        return result;
    }
}