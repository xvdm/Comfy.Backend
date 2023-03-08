using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class Brand : IEntityTypeConfiguration<Brand>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
        }
    }
}
