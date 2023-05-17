using Comfy.Application.Handlers.Categories;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class CategoriesMenuController : BaseController
{
    public CategoriesMenuController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesMenu()
    {
        var result = await Sender.Send(new GetCategoriesMenuQuery());
        return Ok(result);
    }
}