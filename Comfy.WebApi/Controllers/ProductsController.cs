﻿using Comfy.Application.Handlers.Products.CompleteProduct;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByIds;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString;
using Comfy.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public class ProductsController : BaseController
{
    [HttpGet("byId")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var result = await Mediator.Send(new GetProductByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("byUrl")]
    public async Task<IActionResult> GetProduct(string url)
    {
        var result = await Mediator.Send(new GetProductByUrlQuery(url));
        return Ok(result);
    }

    [HttpGet("byIds")]
    public async Task<IActionResult> GetProducts([FromQuery]int[] id)
    {
        var result = await Mediator.Send(new GetProductsByIdsQuery(id));
        return Ok(result);
    }

    [HttpGet("byQuery")]
    public async Task<IActionResult> GetProductsFromQuery(int subcategoryId, string? filterQuery, int? pageNumber, int? pageSize)
    {
        var result = await Mediator.Send(new GetProductsByQueryString(subcategoryId, filterQuery, pageNumber, pageSize));
        return Ok(result);
    }
}