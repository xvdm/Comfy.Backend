namespace Comfy.Domain;

public class Characteristic
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int CharacteristicsNameId { get; set; }
    public CharacteristicName CharacteristicsName { get; set; } = null!;
    
    public int CharacteristicsValueId { get; set; }
    public CharacteristicValue CharacteristicsValue { get; set; } = null!;
}