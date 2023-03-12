using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public partial class QuestionAnswer
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string Text { get; set; } = null!;
        public int UsefullAnswerCount { get; set; }
        public int NeedlessAnswerCount { get; set; }
        public bool IsActive { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
