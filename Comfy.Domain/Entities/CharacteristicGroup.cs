namespace Comfy.Domain.Entities;

public sealed class CharacteristicGroup
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public ICollection<Characteristic> Characteristics { get; set; } = null!;
}