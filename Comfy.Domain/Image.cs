using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class Image : IEntityTypeConfiguration<Image>
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
