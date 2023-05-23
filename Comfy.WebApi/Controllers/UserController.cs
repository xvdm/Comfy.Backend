using Comfy.Application.Common.Helpers;
using Comfy.Application.Handlers.Users;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class UserController : BaseController
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [Authorize(Policy = RoleNames.User)]
    public async Task<IActionResult> GetUserInfo(Guid id)
    {
        var result = await Sender.Send(new GetUserQuery { UserId = id });
        return Ok(result);
    }
}