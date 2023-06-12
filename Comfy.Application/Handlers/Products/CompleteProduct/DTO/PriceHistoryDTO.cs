using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain.Entities;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public sealed record PriceHistoryDTO : IMapWith<PriceHistory>
{
    public decimal Price { get; init; }
    public DateTime Date { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PriceHistory, PriceHistoryDTO>();
    }
}