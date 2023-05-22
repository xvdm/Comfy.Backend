using MediatR;

namespace Comfy.Application.Handlers.Authorization;

public sealed record SignOutQuery : IRequest
{
}

public sealed class SignOutQueryHandler : IRequestHandler<SignOutQuery>
{
    public async Task Handle(SignOutQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
