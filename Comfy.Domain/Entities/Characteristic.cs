﻿namespace Comfy.Domain.Entities;

public sealed class Characteristic
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int CharacteristicGroupId { get; set; }
    public CharacteristicGroup CharacteristicGroup { get; set; } = null!;

    public int CharacteristicsNameId { get; set; }
    public CharacteristicName CharacteristicsName { get; set; } = null!;

    public int CharacteristicsValueId { get; set; }
    public CharacteristicValue CharacteristicsValue { get; set; } = null!;
}