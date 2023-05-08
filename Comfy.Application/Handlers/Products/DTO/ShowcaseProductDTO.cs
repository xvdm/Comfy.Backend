﻿using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Application.Handlers.Products.CompleteProduct.DTO;
using Comfy.Domain.Models;

namespace Comfy.Application.Handlers.Products.DTO;

public record ShowcaseProductDTO : IMapWith<Product>
{
    public string Name { get; init; } = null!;
    public int Price { get; init; }
    public int DiscountAmount { get; init; }
    public int Amount { get; init; }
    public double Rating { get; init; }
    public int ReviewsNumber { get; set; }
    public string Url { get; init; } = null!;

    public IEnumerable<ImageDTO> Images { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ShowcaseProductDTO>();
    }
}