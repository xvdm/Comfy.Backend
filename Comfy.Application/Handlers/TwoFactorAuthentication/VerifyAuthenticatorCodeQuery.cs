using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.LocalizationStrings;
using Comfy.Domain.Identity;
using Google.Authenticator;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.TwoFactorAuthentication;

public sealed record VerifyAuthenticatorCodeQuery(Guid UserId, string Code) : IRequest<bool>;


public sealed class VerifyAuthenticatorCodeQueryHandler : IRequestHandler<VerifyAuthenticatorCodeQuery, bool>
{
    private readonly UserManager<User> _userManager;

    public VerifyAuthenticatorCodeQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(VerifyAuthenticatorCodeQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) throw new NotFoundException(LocalizationStrings.User);

        var key = await _userManager.GetAuthenticatorKeyAsync(user);
        if (key is null) return false;

        var tfa = new TwoFactorAuthenticator();
        var result = tfa.ValidateTwoFactorPIN(key, request.Code);

        return result;
    }
}