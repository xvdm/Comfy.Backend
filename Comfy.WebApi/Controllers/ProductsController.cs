using Comfy.Application.Handlers.Products.CompleteProduct;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByIds;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public class ProductsController : BaseController
{
    public ProductsController(ISender sender) : base(sender)
    {
    }

    [HttpGet("byId")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var result = await Sender.Send(new GetProductByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("byUrl")]
    public async Task<IActionResult> GetProduct(string url)
    {
        var result = await Sender.Send(new GetProductByUrlQuery(url));
        return Ok(result);
    }

    [HttpGet("byIds")]
    public async Task<IActionResult> GetProducts([FromQuery]int[] id)
    {
        var result = await Sender.Send(new GetProductsByIdsQuery(id));
        return Ok(result);
    }

    [HttpGet("byQuery")]
    public async Task<IActionResult> GetProductsFromQuery(int subcategoryId, string? filterQuery, int? pageNumber, int? pageSize)
    {
        var result = await Sender.Send(new GetProductsByQueryString(subcategoryId, filterQuery, pageNumber, pageSize));
        return Ok(result);
    }
}