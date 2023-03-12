using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class Wishlist : IEntityTypeConfiguration<Wishlist>
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(d => d.Products);
        }
    }
}
