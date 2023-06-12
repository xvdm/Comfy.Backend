using Comfy.Application.Handlers.Authorization;
using Comfy.Application.Handlers.Email;
using Comfy.WebApi.Controllers.Base;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class AuthController : BaseController
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Registers a new user with given credentials
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommand command)
    {
        await Sender.Send(command);
        var result = await Sender.Send(new SignInByPasswordCommand(command.Email, command.Password));
        return Ok(result);
    }

    /// <summary>
    /// Returns refreshed JWT access token
    /// </summary>
    [HttpPost("refreshAccessToken")]
    public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshAccessTokenCommand command)
    {
        var jwt = await Sender.Send(command);
        return Ok(jwt);
    }

    /// <summary>
    /// Returns JWT access token, refresh token and user id for the signed in user
    /// </summary>
    [HttpPost("signIn-Password")]
    public async Task<IActionResult> SignInPassword([FromBody] SignInByPasswordCommand query)
    {
        var result = await Sender.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Returns JWT access token, refresh token and user id for the signed in user (through google authentication)
    /// </summary>
    /// <param name="googleIdToken">Google ID token</param>
    [HttpPost("signIn-Google")]
    public async Task<IActionResult> SignInGoogle([FromBody] string googleIdToken)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(googleIdToken);
        var result = await Sender.Send(new SignInGoogleCommand(payload.Email, payload.EmailVerified, payload.Name, payload.Subject));
        return Ok(result);
    }
}