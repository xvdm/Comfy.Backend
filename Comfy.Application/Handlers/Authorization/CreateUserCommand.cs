using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.Helpers;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.Authorization;

public sealed record CreateUserCommand : IRequest<Guid>
{
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
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
        var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userWithEmail is not null) throw new UserWithGivenNameAlreadyExistsException();

        var user = new User
        {
            Email = request.Email,
            Name = request.Name,
            UserName = Guid.NewGuid().ToString()
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded == false) throw new SomethingWrongException();

        await _userManager.AddToRoleAsync(user, RoleNames.User);

        return user.Id;
    }
}