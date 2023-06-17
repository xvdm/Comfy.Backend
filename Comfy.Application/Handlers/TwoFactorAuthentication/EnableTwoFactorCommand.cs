using Comfy.Application.Common.Constants;
using Comfy.Application.Common.Exceptions;
using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.TwoFactorAuthentication;

public sealed record EnableTwoFactorCommand(Guid UserId) : IRequest, IJwtValidation;


public sealed class EnableTwoFactorCommandHandler : IRequestHandler<EnableTwoFactorCommand>
{
    private readonly UserManager<User> _userManager;

    public EnableTwoFactorCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(EnableTwoFactorCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) throw new NotFoundException(LocalizationStrings.User);
        await _userManager.SetTwoFactorEnabledAsync(user, true);
    }
}