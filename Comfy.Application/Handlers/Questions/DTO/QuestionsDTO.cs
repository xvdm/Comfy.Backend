namespace Comfy.Application.Handlers.Questions.DTO;

public record QuestionsDTO
{
    public int ProductId { get; init; }
    public IEnumerable<QuestionDTO> Questions { get; init; } = null!;
}