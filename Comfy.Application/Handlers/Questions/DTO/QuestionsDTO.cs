namespace Comfy.Application.Handlers.Questions.DTO;

public sealed record QuestionsDTO
{
    public int ProductId { get; init; }
    public IEnumerable<QuestionDTO> Questions { get; init; } = null!;
}