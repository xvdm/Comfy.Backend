using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class Characteristic : IEntityTypeConfiguration<Characteristic>
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        //public Product Product { get; set; } = null!;

        public int CharacteristicsNameId { get; set; }
        public CharacteristicName CharacteristicsName { get; set; } = null!;
        
        public int CharacteristicsValueId { get; set; }
        public CharacteristicValue CharacteristicsValue { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Characteristic> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.CharacteristicsName);

            builder.HasOne(d => d.CharacteristicsValue);

            //builder.HasOne(d => d.Product)
            //    .WithMany(p => p.Characteristics)
            //    .HasForeignKey(d => d.ProductId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
