using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class AddressType : IEntityTypeConfiguration<AddressType>
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public ICollection<Address>? Addresses { get; set; }


        public void Configure(EntityTypeBuilder<AddressType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Type).HasMaxLength(50);
        }
    }
}
