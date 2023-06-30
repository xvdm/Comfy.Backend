namespace Comfy.Domain.Entities;

public sealed class CategoryUniqueCharacteristic
{
    public int Id { get; set; }

    public int SubcategoryId { get; set; }
    public Subcategory Subcategory { get; set; } = null!;

    public int CharacteristicNameId { get; set; }
    public CharacteristicName CharacteristicName { get; set; } = null!;

    public int CharacteristicValueId { get; set; }
    public CharacteristicValue CharacteristicValue { get; set; } = null!;
}