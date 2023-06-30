using Comfy.Application.Handlers.Products.CompleteProduct;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByIds;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsBySearchTerm;
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
    /// Returns showcase information about the products (products with searching by name and with sorting)
    /// </summary>
    /// <param name="searchTerm">This is the string that is searched in the product names</param>
    /// <param name="priceFrom">Lower limit of price</param>
    /// <param name="priceTo">Upper limit of price</param>
    /// <param name="sortColumn">Needed for determining the column by which sorting is applied. Possible values: 'name', 'price', 'rating'</param>
    /// <param name="sortOrder">Needed for determining the sort ordering. Possible values: 'desc' for descending order. For ascending order - anything else, including null</param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    [HttpGet("bySearchTerm")]
    public async Task<IActionResult> GetProductsBySearchTerm(string searchTerm, int? priceFrom, int? priceTo, string? sortColumn, string? sortOrder, int? pageNumber, int? pageSize)
    {
        var result = await Sender.Send(new GetProductsBySearchTermQuery(searchTerm, priceFrom, priceTo, sortColumn, sortOrder, pageNumber, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Returns showcase information about the products (products in specific subcategory, with sorting, searching by name and filtering by characteristics, models and brands)
    /// </summary>
    /// <param name="subcategoryId"></param>
    /// <param name="priceFrom">Lower limit of price</param>
    /// <param name="priceTo">Upper limit of price</param>
    /// <param name="sortColumn">Needed for determining the column by which sorting is applied. Possible values: 'name', 'price', 'rating'</param>
    /// <param name="sortOrder">Needed for determining the sort ordering. Possible values: 'desc' for descending order. For ascending order - anything else, including null</param>
    /// <param name="filterQuery">Needed for filtering products by their brand, model or characteristics</param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    [HttpGet]
    public async Task<IActionResult> GetProductsWithFilteringAndSorting(int subcategoryId, int? priceFrom, int? priceTo, string? sortColumn, string? sortOrder, string? filterQuery, int? pageNumber, int? pageSize)
    {
        var result = await Sender.Send(new GetProductsQuery(subcategoryId, priceFrom, priceTo, sortColumn, sortOrder, filterQuery, pageNumber, pageSize));
        return Ok(result);
    }
}