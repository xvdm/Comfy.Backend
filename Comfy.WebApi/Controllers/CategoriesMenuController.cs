using Comfy.Application.Handlers.Categories;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public class CategoriesMenuController : BaseController
{
    private readonly IMediator _mediator;

    public CategoriesMenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesMenu()
    {
        var result = await _mediator.Send(new GetCategoriesMenuQuery());
        return Ok(result);
    }
}