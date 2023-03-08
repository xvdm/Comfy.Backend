using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class OrderedProduct : IEntityTypeConfiguration<OrderedProduct>
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


        public void Configure(EntityTypeBuilder<OrderedProduct> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Order)
                    .WithMany(p => p.OrderedProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Product)
                .WithMany(p => p.OrderedProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
