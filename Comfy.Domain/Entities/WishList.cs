﻿using Comfy.Domain.Identity;

namespace Comfy.Domain.Entities;

public sealed class WishList
{
    public int Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = null!;
}