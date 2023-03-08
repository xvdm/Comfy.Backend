using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class Answer : IEntityTypeConfiguration<Answer>
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int UsefullAnswerCount { get; set; }
        public int NeedlesslyAnswerCount { get; set; }
        public bool IsActive { get; set; }
        
        public int UserId { get; set; }
        //public User User { get; set; } = null!;

        public int TargetId { get; set; }
        public Question TargetQuestion { get; set; } = null!;
        public Review TargetReview { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.TargetQuestion)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.TargetId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.TargetReview)
                .WithMany(p => p.Answers)
                .HasForeignKey(d => d.TargetId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
