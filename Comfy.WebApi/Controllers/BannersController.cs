using Comfy.Application.Handlers.Banners;
using Comfy.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers
{
    public class BannersController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetBanners()
        {
            var result = await Mediator.Send(new GetBannersQuery());
            return Ok(result);
        }
    }
}
