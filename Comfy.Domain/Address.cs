using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class Address : IEntityTypeConfiguration<Address>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Building { get; set; } = null!;
        public int ApartmentsNumber { get; set; }
        public int PostalCode { get; set; }

        public int AddressTypeId { get; set; }
        public AddressType AddressType { get; set; } = null!;

        public ICollection<Order>? Orders { get; set; }


        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.City).HasMaxLength(50);
            builder.Property(e => e.Country).HasMaxLength(50);
            builder.Property(e => e.Street).HasMaxLength(50);

            builder.HasOne(d => d.AddressType)
                .WithMany(p => p.Addresses)
                .HasForeignKey(d => d.AddressTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
