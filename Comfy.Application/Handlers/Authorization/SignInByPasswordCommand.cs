using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Authorization.DTO;
using Comfy.Application.Services.JwtAccessToken;
using Comfy.Application.Services.RefreshTokens;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.Authorization;

public sealed record SignInByPasswordCommand(string Email, string Password) : IRequest<SignInDTO>;


public sealed class SignInByPasswordCommandHandler : IRequestHandler<SignInByPasswordCommand, SignInDTO>
{
    private readonly UserManager<User> _userManager;
    private readonly ICreateJwtAccessTokenService _createJwtAccessTokenService;
    private readonly ICreateRefreshTokenService _createRefreshTokenService;

    public SignInByPasswordCommandHandler(UserManager<User> userManager, ICreateJwtAccessTokenService createJwtAccessTokenService, ICreateRefreshTokenService createRefreshTokenService)
    {
        _userManager = userManager;
        _createJwtAccessTokenService = createJwtAccessTokenService;
        _createRefreshTokenService = createRefreshTokenService;
    }

    public async Task<SignInDTO> Handle(SignInByPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) throw new BadCredentialsException();
        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (isValidPassword == false) throw new BadCredentialsException();

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