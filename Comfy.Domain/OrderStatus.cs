using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class OrderStatus : IEntityTypeConfiguration<OrderStatus>
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public ICollection<Order>? Orders { get; set; }


        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Status).HasMaxLength(50);
        }
    }
}
