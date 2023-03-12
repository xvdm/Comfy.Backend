using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class Question : IEntityTypeConfiguration<Question>
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public int UsefullQuestionCount { get; set; }
        public int NeedlessQuestionCount { get; set; }
        public bool IsActive { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public ICollection<QuestionAnswer> Answers { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreateDate)
                .HasColumnType("date");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
