using Comfy.Application.Handlers.TwoFactorAuthentication;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public class TwoFactorAuthController : BaseController
{
    public TwoFactorAuthController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Sets two-factor enabled
    /// </summary>
    [HttpPost("enableTwoFactor")]
    public async Task<IActionResult> EnableTwoFactor([FromBody] EnableTwoFactorCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    /// <summary>
    /// Sets two-factor disabled
    /// </summary>
    [HttpPost("disableTwoFactor")]
    public async Task<IActionResult> DisableTwoFactor([FromBody] DisableTwoFactorCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    /// <summary>
    /// Returns authenticator key and qr-code url for two-factor authentication setup || JwtValidation
    /// </summary>
    [HttpPost("getAuthSetupInfo")]
    public async Task<IActionResult> GetAuthenticatorSetupInfo([FromBody] GetAuthenticatorSetupInfoQuery query)
    {
        var setupInfo = await Sender.Send(query);
        return Ok(setupInfo);
    }

    /// <summary>
    /// Returns true if given authenticator code is valid
    /// </summary>
    [HttpPost("verifyCode")]
    public async Task<IActionResult> VerifyAuthenticatorCode([FromBody] VerifyAuthenticatorCodeQuery query)
    {
        var result = await Sender.Send(query);
        return Ok(result);
    }
}