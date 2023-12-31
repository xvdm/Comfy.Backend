﻿namespace Comfy.Domain.Entities;

public sealed class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Subcategory> Subcategories { get; set; } = null!;
}