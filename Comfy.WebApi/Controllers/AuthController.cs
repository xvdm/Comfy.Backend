using Comfy.Application.Handlers.Authorization;
using Comfy.Domain.Identity;
using Comfy.WebApi.Controllers.Base;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class AuthController : BaseController
{
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    public AuthController(ISender sender, SignInManager<User> signInManager, IConfiguration configuration) : base(sender)
    {
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserCommand command)
    {
        await Sender.Send(command);
        var result = await Sender.Send(new SignInByPasswordCommand(command.Email, command.Password));
        return Ok(result);
    }

    [HttpPost("refreshAccessToken")]
    public async Task<IActionResult> RefreshAccessToken(RefreshAccessTokenCommand command)
    {
        var jwt = await Sender.Send(command);
        return Ok(jwt);
    }

    [HttpPost("signIn-Password")]
    public async Task<IActionResult> SignInPassword(SignInByPasswordCommand query)
    {
        var result = await Sender.Send(query);
        return Ok(result);
    }

    [HttpPost("signIn-Google")]
    public async Task<IActionResult> SignInGoogle(string googleIdToken)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(googleIdToken);

        var result = await Sender.Send(new SignInGoogleCommand(payload.Email, payload.EmailVerified, payload.Name, payload.Subject));

        return Ok(result);
    }
}