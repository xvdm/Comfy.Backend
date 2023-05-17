namespace Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.DTO;

public sealed record CharacteristicDTO
{
    public CharacteristicNameDTO Name { get; init; } = null!;
    public IList<CharacteristicValueDTO> Values { get; init; } = null!;
}