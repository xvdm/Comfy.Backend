using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class CharacteristicName : IEntityTypeConfiguration<CharacteristicName>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;


        public void Configure(EntityTypeBuilder<CharacteristicName> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
