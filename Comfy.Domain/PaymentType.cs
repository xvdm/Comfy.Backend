using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class PaymentType : IEntityTypeConfiguration<PaymentType>
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public ICollection<Order>? Orders { get; set; }

        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
