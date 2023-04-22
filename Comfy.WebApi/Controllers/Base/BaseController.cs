using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers.Base;

[ApiController]
//[Route("api/[controller]/[action]")]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>()!;
    //internal Guid UserId => !User.Identity.IsAuthenticated ? Guid.Empty : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
}