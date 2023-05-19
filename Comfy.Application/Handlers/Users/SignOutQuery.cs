using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Application.Handlers.Users;

public sealed record SignOutQuery : IRequest
{
}

public sealed class SignOutQueryHandler : IRequestHandler<SignOutQuery>
{
    private readonly SignInManager<User> _signInManager;

    public SignOutQueryHandler(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task Handle(SignOutQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
