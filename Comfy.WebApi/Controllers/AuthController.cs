using Comfy.Application.Common.Helpers;
using Comfy.Application.Handlers.Users;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class AuthController : BaseController
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpGet("getInfo")]
    public async Task<IActionResult> Index()
    {
        await Task.CompletedTask;
        return Ok("info");
    }

    [HttpGet("getInfoForUser")]
    [Authorize(Policy = RoleNames.User)]
    public async Task<IActionResult> AuthUser()
    {
        await Task.CompletedTask;
        return Ok("info for user");
    }

    [HttpGet("getInfoForAdmin")]
    [Authorize(Policy = RoleNames.Administrator)]
    public async Task<IActionResult> AuthAdmin()
    {
        await Task.CompletedTask;
        return Ok("info for admin");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserCommand command)
    {
        var result = await Sender.Send(command);
        var resultJwt = "";
        if (result != Guid.Empty)
        {
            resultJwt = await Sender.Send(new SignInByPasswordQuery { Username = command.Username, Password = command.Password });
        }
        return Ok($"{resultJwt}");
    }

    [HttpPost("signIn")]
    public async Task<IActionResult> LogIn(SignInByPasswordQuery query)
    {
        var jwt = await Sender.Send(query);
        return Ok($"{jwt}");
    }

    [HttpPost("signOut")]
    public async Task<IActionResult> LogOut()
    {
        //await Sender.Send(new SignOutQuery());
        return Ok("Signed out");
    }
}