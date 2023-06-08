using Comfy.Application.Handlers.Banners;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class BannersController : BaseController
{
    public BannersController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Returns banners for the home page carousel
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetBanners()
    {
        var result = await Sender.Send(new GetBannersQuery());
        return Ok(result);
    }
}