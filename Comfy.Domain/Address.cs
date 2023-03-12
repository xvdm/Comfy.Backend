using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class Address : IEntityTypeConfiguration<Address>
    {
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Building { get; set; } = null!;
        public int ApartmentsNumber { get; set; }
        public int PostalCode { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public int AddressTypeId { get; set; }
        public AddressType AddressType { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.City).HasMaxLength(50);
            builder.Property(e => e.Country).HasMaxLength(50);
            builder.Property(e => e.Street).HasMaxLength(50);

            builder.HasOne(d => d.AddressType);
        }
    }
}
