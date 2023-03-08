using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class Wishlist : IEntityTypeConfiguration<Wishlist>
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        //public User User { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Product)
                    .WithMany(p => p.WhishLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
