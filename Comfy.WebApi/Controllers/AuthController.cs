using Comfy.Application.Handlers.Authorization;
using Comfy.Domain.Identity;
using Comfy.WebApi.Controllers.Base;
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
        var result = await Sender.Send(new SignInByPasswordQuery(command.Email, command.Password));
        return Ok(result);
    }

    [HttpPost("refreshAccessToken")]
    public async Task<IActionResult> RefreshAccessToken(RefreshAccessTokenCommand command)
    {
        var jwt = await Sender.Send(command);
        return Ok(jwt);
    }

    [HttpPost("signIn-Password")]
    public async Task<IActionResult> SignInPassword(SignInByPasswordQuery query)
    {
        var result = await Sender.Send(query);
        return Ok(result);
    }

    [HttpPost("signIn-External")]
    public async Task<IActionResult> SignInExternal()
    {
        string jwt = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjYwODNkZDU5ODE2NzNmNjYxZmRlOWRhZTY0NmI2ZjAzODBhMDE0NWMiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJuYmYiOjE2ODU1MzEwNjksImF1ZCI6IjU2NTc5NzI0Nzk5Ny02OGo2cGVqMXB1NzNraTNpZWhpbDFvNXN2Z2lzdWhzZC5hcHBzLmdvb2dsZXVzZXJjb250ZW50LmNvbSIsInN1YiI6IjEwMDQ3MDgzMzU3MDk0OTE3MTQzNiIsImVtYWlsIjoiMWxhdmFib3gxQGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJhenAiOiI1NjU3OTcyNDc5OTctNjhqNnBlajFwdTcza2kzaWVoaWwxbzVzdmdpc3Voc2QuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJuYW1lIjoidGFwZWV4IiwicGljdHVyZSI6Imh0dHBzOi8vbGgzLmdvb2dsZXVzZXJjb250ZW50LmNvbS9hL0FBY0hUdGQ4MXRfbU5Hc3hYR1Rvb3NDb3BzUnlBM3BRLVBnLV9JYTBHVzhLMUE9czk2LWMiLCJnaXZlbl9uYW1lIjoidGFwZWV4IiwiaWF0IjoxNjg1NTMxMzY5LCJleHAiOjE2ODU1MzQ5NjksImp0aSI6ImEwODU5YzdkNGFmZDY1ZGQ5NmEwOGE2YTgwYjE0Y2VkNWY3ZmNmNzUifQ.eYkto5YS6nnt7NTYWF-_ta2Jdz0x4Q1Z78vq64SMK31ViRc2dqiLNIsZRGWJhwrJvrCv_Vt6vdHcQ-j9uFJ4N4I9I5CqMnujQynpvqm5KS8-RTQGL1qm5GzN8e2P01uvbXC6G4-nyyaPLXg-KxvBS9m1fsh01_aDfdgk66uStZqFIRNCdq9G1YlhirmZBviYh3MmOfZeR1NN2DLuHUsw6yiGowo350wCYxPgT_bzuGIRI-uQ6jmqSpCt1Qyx2gYR4kBeyvVwCWJaJEbaSE96wE71nWPukN-LUDynoy7caT2gOpJ6Opg0uhMyOVlx6qZkoovMJ_U6PzaYs9pvUuE7-g";

        await Task.CompletedTask;
        return Ok();
    }
}