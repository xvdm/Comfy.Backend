using Comfy.Application.Handlers.Orders;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class OrdersController : BaseController
{
    public OrdersController(ISender sender) : base(sender)
    {
    }


    /// <summary>
    /// Creates and order || JwtValidation
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }
}