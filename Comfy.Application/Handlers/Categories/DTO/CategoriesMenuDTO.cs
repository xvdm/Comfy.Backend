namespace Comfy.Application.Handlers.Categories.DTO;

public record CategoriesMenuDTO
{
    public IEnumerable<MainCategoryDTO> MainCategories { get; init; } = null!;
}