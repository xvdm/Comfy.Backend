using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class Subcategory : IEntityTypeConfiguration<Subcategory>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; } = null!;

        public ISet<Characteristic> UniqueCharacteristics { get; set; } = null!;
        public ISet<Brand> UniqueBrands { get; set; } = null!;

        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsUnicode(false);

            builder.HasOne(x => x.MainCategory)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.MainCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(x => x.UniqueCharacteristics);
            builder.HasMany(x => x.UniqueBrands);
        }
    }
}
