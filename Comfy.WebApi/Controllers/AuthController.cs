using Comfy.Application.Handlers.Authorization;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class AuthController : BaseController
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserCommand command)
    {
        await Sender.Send(command);
        var result = await Sender.Send(new SignInByPasswordQuery { Username = command.Username, Password = command.Password });
        return Ok(result);
    }

    [HttpPost("signIn")]
    public async Task<IActionResult> LogIn(SignInByPasswordQuery query)
    {
        var result = await Sender.Send(query);
        return Ok(result);
    }

    [HttpPost("signOut")]
    public async Task<IActionResult> LogOut()
    {
        //await Sender.Send(new SignOutQuery());
        return Ok("Signed out");
    }
}