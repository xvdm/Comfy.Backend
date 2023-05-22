using Comfy.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Comfy.Application.Handlers.JWTValidation;

public sealed record ValidateUserIdQuery(Guid UserId) : IRequest;


public sealed class ValidateUserIdQueryHandler : IRequestHandler<ValidateUserIdQuery>
{
    private readonly IHttpContextAccessor _contextAccessor;

    public ValidateUserIdQueryHandler(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task Handle(ValidateUserIdQuery request, CancellationToken cancellationToken)
    {
        if (_contextAccessor.HttpContext is null) throw new UnauthorizedException();

        var sub = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        if (sub != request.UserId.ToString()) throw new UnauthorizedException();
    }
}