using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.LocalizationStrings;
using Comfy.Application.Handlers.TwoFactorAuthentication.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using Google.Authenticator;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.TwoFactorAuthentication;

public sealed record GetAuthenticatorSetupInfoQuery(Guid UserId) : IRequest<SetupInfoDTO>, IJwtValidation;


public sealed class GetAuthenticatorSetupInfoQueryQueryHandler : IRequestHandler<GetAuthenticatorSetupInfoQuery, SetupInfoDTO>
{
    private readonly UserManager<User> _userManager;

    public GetAuthenticatorSetupInfoQueryQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<SetupInfoDTO> Handle(GetAuthenticatorSetupInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) throw new NotFoundException(LocalizationStrings.User);

        var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
        if (authenticatorKey is null)
        {
            await _userManager.ResetAuthenticatorKeyAsync(user);
            authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
        }

        var tfa = new TwoFactorAuthenticator();
        var setupInfo = tfa.GenerateSetupCode("Loffy", user.Email, authenticatorKey, false);

        var result = new SetupInfoDTO(setupInfo.QrCodeSetupImageUrl, setupInfo.ManualEntryKey);

        return result;
    }
}