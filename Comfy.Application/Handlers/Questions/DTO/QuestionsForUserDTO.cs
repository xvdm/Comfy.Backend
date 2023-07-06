namespace Comfy.Application.Handlers.Questions.DTO;

public sealed record QuestionsForUserDTO
{
    public Guid UserId { get; init; }
    public int TotalQuestionsNumber { get; init; }
    public IEnumerable<QuestionDTO> Questions { get; init; } = null!;
}