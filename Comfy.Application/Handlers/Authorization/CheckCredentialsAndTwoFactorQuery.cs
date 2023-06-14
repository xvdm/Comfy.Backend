using Comfy.Application.Handlers.Authorization.DTO;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.Authorization;

public sealed record CheckCredentialsAndTwoFactorQuery(string Email, string Password) : IRequest<VerifiedCredentialsDTO>;


public sealed class CheckCredentialsAndTwoFactorQueryHandler : IRequestHandler<CheckCredentialsAndTwoFactorQuery, VerifiedCredentialsDTO>
{
    private readonly UserManager<User> _userManager;

    public CheckCredentialsAndTwoFactorQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<VerifiedCredentialsDTO> Handle(CheckCredentialsAndTwoFactorQuery request, CancellationToken cancellationToken)
    {
        var result = new VerifiedCredentialsDTO();

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return result;

        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (isValidPassword == false) return result;

        var twoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
        result.ValidCredentials = true;
        result.TwoFactorEnabled = twoFactorEnabled;
        result.UserId = user.Id;

        return result;
    }
}