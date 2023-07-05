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
    /// Creates an order || JwtValidation
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    /// <summary>
    /// Get orders for user || JwtValidation
    /// </summary>
    [HttpGet("forUser")]
    public async Task<IActionResult> GetOrdersForUser([FromQuery]GetOrdersForUserQuery query)
    {
        var orders = await Sender.Send(query);
        return Ok(orders);
    }
}