using Comfy.Application.Handlers.WishLists;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class WishListController : BaseController
{
    public WishListController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Get products in user's wishList
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetWishListProducts([FromQuery] GetUserWishListProductsQuery query)
    {
        var result = await Sender.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Add product to user wishList || JwtValidation
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddProductToWishList(AddProductToWishListCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    /// <summary>
    /// Removes product from user's wishList
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> RemoveProductFromWishList(RemoveProductFromWishListCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }
}