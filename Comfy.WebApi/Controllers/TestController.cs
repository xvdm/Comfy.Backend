using Comfy.Domain.Identity;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class TestController : BaseController
{
    private readonly UserManager<User> _userManager;

    public TestController(ISender sender, UserManager<User> userManager) : base(sender)
    {
        _userManager = userManager;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        await _userManager.DeleteAsync(user);
        return Ok();
    }
}