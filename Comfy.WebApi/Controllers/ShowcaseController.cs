using Comfy.Application.Handlers.Products.HomepageShowcase;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public class ShowcaseController : BaseController
{
    private readonly IMediator _mediator;

    public ShowcaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetShowcaseGroups()
    {
        var result = await _mediator.Send(new GetShowcaseGroupsQuery());
        return Ok(result);
    }
}