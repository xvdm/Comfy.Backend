using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class PriceHistory : IEntityTypeConfiguration<PriceHistory>
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


        public void Configure(EntityTypeBuilder<PriceHistory> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("PriceHistory");
            builder.Property(e => e.Date).HasColumnType("date");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.PriceHistory)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
