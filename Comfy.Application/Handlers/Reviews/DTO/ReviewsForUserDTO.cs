namespace Comfy.Application.Handlers.Reviews.DTO;

public sealed record ReviewsForUserDTO
{
    public Guid UserId { get; init; }
    public int TotalReviewsNumber { get; init; }
    public IEnumerable<ReviewDTO> Reviews { get; init; } = null!;
}