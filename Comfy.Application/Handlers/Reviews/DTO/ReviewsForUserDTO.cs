namespace Comfy.Application.Handlers.Reviews.DTO;

public sealed record ReviewsForUserDTO
{
    public Guid UserId { get; init; }
    public int TotalReviewsNumber { get; init; }
    public IEnumerable<ReviewForUserDTO> Reviews { get; init; } = null!;
}