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
        var result = await Sender.Send(new SignInByPasswordQuery(command.Username, command.Password));
        return Ok(result);
    }

    [HttpPost("signIn")]
    public async Task<IActionResult> LogIn(SignInByPasswordQuery query)
    {
        var result = await Sender.Send(query);
        return Ok(result);
    }

    [HttpPost("refreshAccessToken")]
    public async Task<IActionResult> RefreshAccessToken(RefreshAccessTokenCommand command)
    {
        var jwt = await Sender.Send(command);
        return Ok(jwt);
    }
}