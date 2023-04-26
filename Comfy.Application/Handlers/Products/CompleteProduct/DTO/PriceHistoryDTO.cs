using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public record PriceHistoryDTO : IMapWith<PriceHistory>
{
    public int Price { get; set; }
    public DateTime Date { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PriceHistory, PriceHistoryDTO>();
    }
}