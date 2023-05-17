﻿namespace Comfy.Domain.Models;

public sealed class ShowcaseGroup
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string QueryString { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = null!;
}