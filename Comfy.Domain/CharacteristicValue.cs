using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Domain
{
    public class CharacteristicValue : IEntityTypeConfiguration<CharacteristicValue>
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;


        public void Configure(EntityTypeBuilder<CharacteristicValue> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
