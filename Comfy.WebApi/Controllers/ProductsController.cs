using Comfy.Application.Handlers.Products.CompleteProduct;
using Comfy.Application.Handlers.Products.ShowcaseProducts;
using Comfy.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public class ProductsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetProduct(int id)
    {
        var result = await Mediator.Send(new GetProductQuery(id));
        return Ok(result);
    }

    [HttpGet("byIds")]
    public async Task<IActionResult> GetProducts([FromQuery]int[] id)
    {
        var result = await Mediator.Send(new GetProductsByIdsQuery(id));
        return Ok(result);
    }


    //[HttpGet("categoryId/{categoryId:int}")]
    //public async Task<IActionResult> GetAllWithCategoryAndFilter(int categoryId, string? filterQuery)
    //{
    //    if (ProductUrl.TryRemoveEmptyAndDuplicatesFromQuery(filterQuery, out var queryDictionary))
    //    {
    //        filterQuery = ProductUrl.GetUrlQuery(queryDictionary);
    //    }
    //
    //    var category = await Mediator.Send(new GetSubcategoryByIdQuery(categoryId));
    //    if (category is null)
    //    {
    //        return NotFound(category);
    //    }
    //    var characteristicsDictionary = GetCharacteristicsInCategory(category);
    //    var products = await Mediator.Send(new GetProductsQuery(categoryId, queryDictionary));
    //    var brands = await Mediator.Send(new GetBrandsQuery(categoryId));
    //
    //    var viewModel = new ProductsViewModel()
    //    {
    //        CategoryId = category.Id,
    //        Characteristics = characteristicsDictionary,
    //        Query = filterQuery,
    //        Products = products,
    //        Brands = brands
    //    };
    //
    //    return Ok(viewModel);
    //}


    //private static Dictionary<CharacteristicName, List<CharacteristicValue>> GetCharacteristicsInCategory(Subcategory category)
    //{
    //    var characteristicsDictionary = new Dictionary<CharacteristicName, List<CharacteristicValue>>();
    //    foreach (var characteristic in category.UniqueCharacteristics)
    //    {
    //        if (characteristicsDictionary.TryGetValue(characteristic.CharacteristicsName, out List<CharacteristicValue>? characteristicValues))
    //        {
    //            characteristicValues.Add(characteristic.CharacteristicsValue);
    //        }
    //        else
    //        {
    //            var list = new List<CharacteristicValue>
    //            {
    //                characteristic.CharacteristicsValue
    //            };
    //            characteristicsDictionary.Add(characteristic.CharacteristicsName, list);
    //        }
    //    }
    //    return characteristicsDictionary;
    //}
}