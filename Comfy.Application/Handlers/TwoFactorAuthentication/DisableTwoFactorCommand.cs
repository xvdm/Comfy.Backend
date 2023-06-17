using Comfy.Application.Common.Constants;
using Comfy.Application.Common.Exceptions;
using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.TwoFactorAuthentication;

public sealed record DisableTwoFactorCommand(Guid UserId) : IRequest, IJwtValidation;


public sealed class DisableTwoFactorCommandHandler : IRequestHandler<DisableTwoFactorCommand>
{
    private readonly UserManager<User> _userManager;

    public DisableTwoFactorCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(DisableTwoFactorCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) throw new NotFoundException(LocalizationStrings.User);
        await _userManager.SetTwoFactorEnabledAsync(user, false);
    }
}