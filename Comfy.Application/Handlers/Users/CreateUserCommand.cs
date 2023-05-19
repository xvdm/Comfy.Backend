using System.Security.Claims;
using Comfy.Application.Common.Helpers;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.Users;

public sealed record CreateUserCommand : IRequest<Guid>
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}


public sealed record CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public CreateUserCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.Username
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded == false) return Guid.Empty;

        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, PoliciesNames.User));

        return user.Id;
    }
}