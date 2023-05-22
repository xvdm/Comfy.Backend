using Comfy.Application.Common.Exceptions;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Comfy.Application.Handlers.JWTValidation;

public sealed record ValidateUserSecurityStampQuery(Guid UserId) : IRequest;


public sealed class ValidateUserSecurityStampQueryHandler : IRequestHandler<ValidateUserSecurityStampQuery>
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public ValidateUserSecurityStampQueryHandler(UserManager<User> userManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public async Task Handle(ValidateUserSecurityStampQuery request, CancellationToken cancellationToken)
    {
        if (_contextAccessor.HttpContext is null) throw new UnauthorizedException();

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) throw new UnauthorizedException();
        
        var stamp = await _userManager.GetSecurityStampAsync(user);
        if(stamp is null) throw new UnauthorizedException();

        var tokenStamp = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
        if(tokenStamp != stamp) throw new UnauthorizedException();
    }
}
