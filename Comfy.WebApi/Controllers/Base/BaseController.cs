using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected BaseController(ISender sender)
    {
        Sender = sender;
    }

    protected ISender Sender { get; }
}