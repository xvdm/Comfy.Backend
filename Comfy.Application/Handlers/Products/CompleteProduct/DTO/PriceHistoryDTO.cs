using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public record PriceHistoryDTO : IMapWith<PriceHistory>
{
    public int Price { get; init; }
    public DateTime Date { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PriceHistory, PriceHistoryDTO>();
    }
}