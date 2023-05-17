namespace Comfy.Application.Handlers.Categories.DTO;

public sealed record CategoriesMenuDTO
{
    public IEnumerable<MainCategoryDTO> MainCategories { get; init; } = null!;
}