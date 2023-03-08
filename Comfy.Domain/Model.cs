using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class Model : IEntityTypeConfiguration<Model>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                    .IsUnicode(false);
        }
    }
}
