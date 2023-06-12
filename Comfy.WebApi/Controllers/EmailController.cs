using Comfy.Application.Handlers.Authorization;
using Comfy.Application.Handlers.Email;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class EmailController : BaseController
{
    public EmailController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Sends a confirmation code to the email address
    /// </summary>
    [HttpPost("sendRegistrationEmail")]
    public async Task<IActionResult> RegisterPendingUserAndSendEmail([FromBody] CreatePendingUserCommand command)
    {
        var confirmationCode = await Sender.Send(command);
        await Sender.Send(new SendConfirmationEmailCommand(command.Email, confirmationCode));
        return Ok();
    }

    /// <summary>
    /// Returns true if entered code is valid (equals to the code that was sent to the email address)
    /// </summary>
    [HttpPost("confirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand command)
    {
        var emailConfirmed = await Sender.Send(command);
        return Ok(new { EmailConfirmed = emailConfirmed });
    }
}