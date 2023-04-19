﻿namespace Comfy.Domain;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Subcategory> Subcategories { get; set; } = null!;
}