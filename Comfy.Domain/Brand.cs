using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comfy.Domain
{
    public class Brand : IEntityTypeConfiguration<Brand>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Subcategory> Subcategories { get; set; } = null!;

        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasMany(x => x.Subcategories)
                .WithMany(x => x.UniqueBrands);
        }
    }
}
