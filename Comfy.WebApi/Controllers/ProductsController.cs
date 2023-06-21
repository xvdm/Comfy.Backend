using Comfy.Application.Handlers.Products.CompleteProduct;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByIds;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class ProductsController : BaseController
{
    public ProductsController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Returns all information about the product
    /// </summary>
    [HttpGet("byId")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var result = await Sender.Send(new GetProductByIdQuery(id));
        return Ok(result);
    }

    /// <summary>
    /// Returns all information about the product
    /// </summary>
    [HttpGet("byUrl")]
    public async Task<IActionResult> GetProduct(string url)
    {
        var result = await Sender.Send(new GetProductByUrlQuery(url));
        return Ok(result);
    }

    /// <summary>
    /// Returns showcase information about the products (for recently watched products)
    /// </summary>
    [HttpGet("byIds")]
    public async Task<IActionResult> GetProducts([FromQuery]int[] id)
    {
        var result = await Sender.Send(new GetProductsByIdsQuery(id));
        return Ok(result);
    }

    /// <summary>
    /// Returns showcase information about the products (products in specific subcategory, with sorting, searching by name and filtering by characteristics, models and brands)
    /// </summary>
    /// <param name="searchTerm">Needed for searching by products names</param>
    /// <param name="sortColumn">Needed for determining the column by which sorting is applied. Possible values: 'name', 'price', 'rating'</param>
    /// <param name="sortOrder">Needed for determining the sort ordering. Possible values: 'desc' for descending order. For ascending order - anything else, including null</param>
    /// <param name="filterQuery">Needed for filtering products by their brand, model or characteristics</param>

    [HttpGet]
    public async Task<IActionResult> GetProductsWithFilteringAndSorting(int subcategoryId, string? searchTerm, string? sortColumn, string? sortOrder, string? filterQuery, int? pageNumber, int? pageSize)
    {
        var result = await Sender.Send(new GetProductsQuery(subcategoryId, searchTerm, sortColumn, sortOrder, filterQuery, pageNumber, pageSize));
        return Ok(result);
    }
}