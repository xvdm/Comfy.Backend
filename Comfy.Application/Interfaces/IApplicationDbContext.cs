﻿using Comfy.Domain.Entities;
using Comfy.Domain.Logging;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<UserLog> UserLogs { get; set; }
    public DbSet<LoggingAction> LoggingActions { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<MainCategory> MainCategories { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<CharacteristicGroup> CharacteristicGroups { get; set; }
    public DbSet<CharacteristicName> CharacteristicsNames { get; set; }
    public DbSet<CharacteristicValue> CharacteristicsValues { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<PriceHistory> PriceHistories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ReviewAnswer> ReviewAnswers { get; set; }
    public DbSet<WishList> WishLists { get; set; }
    public DbSet<SubcategoryFilter> SubcategoryFilters { get; set; }
    public DbSet<ShowcaseGroup> ShowcaseGroups { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<PendingUser> PendingUsers { get; set; }
    public DbSet<CategoryUniqueCharacteristic> CategoryUniqueCharacteristics { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}