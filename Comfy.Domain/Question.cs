using Comfy.Domain.Base;

namespace Comfy.Domain
{
    public class Question : Auditable
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int UsefulQuestionCount { get; set; }
        public int NeedlessQuestionCount { get; set; }
        public bool IsActive { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public ICollection<QuestionAnswer> Answers { get; set; } = null!;
    }
}
