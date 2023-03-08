using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class MainCategory : IEntityTypeConfiguration<MainCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Subcategory> Categories { get; set; } = null!;

        public void Configure(EntityTypeBuilder<MainCategory> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsUnicode(false);
        }
    }
}
