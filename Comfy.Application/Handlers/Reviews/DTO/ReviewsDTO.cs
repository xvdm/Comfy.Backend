namespace Comfy.Application.Handlers.Reviews.DTO;

public sealed record ReviewsDTO
{
    public int ProductId { get; init; }
    public int TotalReviewsNumber { get; init; }
    public IEnumerable<ReviewDTO> Reviews { get; init; } = null!;
}