using Comfy.Domain.Logging;
using Comfy.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<UserLog> UserLogs { get; set; }
    public DbSet<LoggingAction> LoggingActions { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<MainCategory> MainCategories { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<CharacteristicName> CharacteristicsNames { get; set; }
    public DbSet<CharacteristicValue> CharacteristicsValues { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<PriceHistory> PriceHistories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ReviewAnswer> ReviewAnswers { get; set; }
    public DbSet<WishList> WishLists { get; set; }
    public DbSet<MainCategoryImage> MainCategoryImages { get; set; }
    public DbSet<SubcategoryImage> SubcategoryImages { get; set; }
    public DbSet<SubcategoryFilter> SubcategoryFilters { get; set; }
    public DbSet<ShowcaseGroup> ShowcaseGroups { get; set; }
    public DbSet<Banner> Banners { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

