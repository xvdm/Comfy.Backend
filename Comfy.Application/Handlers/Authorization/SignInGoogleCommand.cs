using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.Helpers;
using Comfy.Application.Handlers.Authorization.DTO;
using Comfy.Application.Services.JwtAccessToken;
using Comfy.Application.Services.RefreshTokens;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Comfy.Application.Handlers.Authorization;

public sealed record SignInGoogleCommand(string Email, bool EmailVerified, string Name, string Subject) : IRequest<SignInDTO>;


public sealed class SignInGoogleCommandHandler : IRequestHandler<SignInGoogleCommand, SignInDTO>
{
    private readonly UserManager<User> _userManager;
    private readonly ICreateJwtAccessTokenService _createJwtAccessTokenService;
    private readonly ICreateRefreshTokenService _createRefreshTokenService;

    public SignInGoogleCommandHandler(UserManager<User> userManager, ICreateJwtAccessTokenService createJwtAccessTokenService, ICreateRefreshTokenService createRefreshTokenService)
    {
        _userManager = userManager;
        _createJwtAccessTokenService = createJwtAccessTokenService;
        _createRefreshTokenService = createRefreshTokenService;
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
        var refreshToken = await _createRefreshTokenService.CreateToken(user.Id, cancellationToken);
        var result = new SignInDTO
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        return result;
    }
} 