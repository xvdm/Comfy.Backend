using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.Helpers;
using Comfy.Application.Handlers.Authorization.DTO;
using Comfy.Application.Interfaces;
using Comfy.Application.Services.JwtAccessToken;
using Comfy.Domain.Identity;
using Comfy.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Comfy.Application.Handlers.Authorization;

public sealed record SignInGoogleCommand(string Email, bool EmailVerified, string Name, string Subject) : IRequest<SignInDTO>;


public sealed class SignInGoogleCommandHandler : IRequestHandler<SignInGoogleCommand, SignInDTO>
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly ICreateJwtAccessTokenService _createJwtAccessTokenService;
    private readonly IConfiguration _configuration;

    public SignInGoogleCommandHandler(UserManager<User> userManager, IApplicationDbContext context, ICreateJwtAccessTokenService createJwtAccessTokenService, IConfiguration configuration)
    {
        _userManager = userManager;
        _context = context;
        _createJwtAccessTokenService = createJwtAccessTokenService;
        _configuration = configuration;
    }

    public async Task<SignInDTO> Handle(SignInGoogleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByLoginAsync(ExternalProviders.Google, request.Subject);
        if (user is null)
        {
            user = new User
            {
                UserName = Guid.NewGuid().ToString(),
                Email = request.Email,
                Name = request.Name,
                EmailConfirmed = request.EmailVerified
            };
            var userCreateResult = await _userManager.CreateAsync(user);
            if (userCreateResult.Succeeded == false) throw new SomethingWrongException();

            var loginInfo = new ExternalLoginInfo(new ClaimsPrincipal(), ExternalProviders.Google, request.Subject, ExternalProviders.Google);
            await _userManager.AddLoginAsync(user, loginInfo);
            await _userManager.AddToRoleAsync(user, RoleNames.User);
        }

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