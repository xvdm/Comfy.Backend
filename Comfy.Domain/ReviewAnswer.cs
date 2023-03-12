using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public partial class ReviewAnswer
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string Text { get; set; } = null!;
        public int UsefullAnswerCount { get; set; }
        public int NeedlessAnswerCount { get; set; }
        public bool IsActive { get; set; }

        public int ReviewId { get; set; }
        public Review Review { get; set; } = null!;

        public void Configure(EntityTypeBuilder<ReviewAnswer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Review)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
