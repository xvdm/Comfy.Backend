using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.Helpers;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.Authorization;

public sealed record CreateUserCommand : IRequest<Guid>
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}


public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly UserManager<User> _userManager;

    public CreateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userWithName = await _userManager.FindByNameAsync(request.Username);
        if (userWithName is not null) throw new UserWithGivenNameAlreadyExistsException();

        var user = new User { UserName = request.Username };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded == false) throw new SomethingWrongException();

        await _userManager.AddToRoleAsync(user, RoleNames.User);

        return user.Id;
    }
}