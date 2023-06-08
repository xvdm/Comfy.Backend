using Comfy.Application.Handlers.Products.HomepageShowcase;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class ShowcaseController : BaseController
{
    public ShowcaseController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Returns product groups for a showcase with showcase product information for each group
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetShowcaseGroups()
    {
        var result = await Sender.Send(new GetShowcaseGroupsQuery());
        return Ok(result);
    }
}