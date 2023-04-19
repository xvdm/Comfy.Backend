using Comfy.Application.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Comfy.Persistence.Helpers;
using Comfy.Domain;
using Comfy.Domain.ViewModels;

namespace Comfy.WebApi.Controllers;

public class ProductsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllWithCategoryAndFilter(int categoryId, string? filterQuery)
    {
        if (ProductUrl.TryRemoveEmptyAndDuplicatesFromQuery(filterQuery, out Dictionary<string, List<string>> queryDictionary))
        {
            return LocalRedirect($"/Product/GetAllWithCategoryAndFilter?categoryId={categoryId}&filterQuery={WebUtility.UrlEncode(ProductUrl.GetUrlQuery(queryDictionary))}");
        }
        
        var category = await Mediator.Send(new GetSubcategoryByIdQuery(categoryId));
        if (category is null)
        {
            return NotFound(category);
        }
        var characteristicsDictionary = GetCharacteristicsInCategory(category);
        var products = await Mediator.Send(new GetProductsQuery(categoryId, queryDictionary));
        var brands = await Mediator.Send(new GetBrandsQuery(categoryId));

        var viewModel = new ProductsViewModel()
        {
            CategoryId = category.Id,
            Characteristics = characteristicsDictionary,
            Query = filterQuery,
            Products = products,
            Brands = brands
        };

        return Ok(viewModel);
    }


    [HttpGet]
    public async Task<IActionResult> GetBrandsByCategory(int category)
    {
        var result = await Mediator.Send(new GetBrandsQuery(category));
        return Ok(result);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await Mediator.Send(new GetAllCategoriesQuery());
        return Ok(result);
    }


    private static Dictionary<CharacteristicName, List<CharacteristicValue>> GetCharacteristicsInCategory(Subcategory category)
    {
        var characteristicsDictionary = new Dictionary<CharacteristicName, List<CharacteristicValue>>();
        foreach (var characteristic in category.UniqueCharacteristics)
        {
            if (characteristicsDictionary.TryGetValue(characteristic.CharacteristicsName, out List<CharacteristicValue>? characteristicValues))
            {
                characteristicValues.Add(characteristic.CharacteristicsValue);
            }
            else
            {
                var list = new List<CharacteristicValue>();
                list.Add(characteristic.CharacteristicsValue);
                characteristicsDictionary.Add(characteristic.CharacteristicsName, list);
            }
        }
        return characteristicsDictionary;
    }
}