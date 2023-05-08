namespace Comfy.Application.Handlers.Reviews.DTO;

public record ReviewsDTO
{
    public int ProductId { get; init; }
    public IEnumerable<ReviewDTO> Reviews { get; init; } = null!;
}