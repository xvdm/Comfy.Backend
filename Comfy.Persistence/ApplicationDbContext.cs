using Comfy.Application.Interfaces;
using Comfy.Domain.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Comfy.Domain.Identity;
using Comfy.Domain.Logging;
using Comfy.Domain;

namespace Comfy.Persistence;

public class ApplicationDbContext : IdentityDbContext<User, ApplicationRole, Guid>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<UserLog> UserLogs { get; set; } = null!;
    public DbSet<LoggingAction> LoggingActions { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<AddressType> AddressTypes { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<MainCategory> MainCategories { get; set; } = null!;
    public DbSet<Subcategory> Subcategories { get; set; } = null!;
    public DbSet<Characteristic> Characteristics { get; set; } = null!;
    public DbSet<CharacteristicName> CharacteristicsNames { get; set; } = null!;
    public DbSet<CharacteristicValue> CharacteristicsValues { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<Model> Models { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
    public DbSet<PaymentType> PaymentTypes { get; set; } = null!;
    public DbSet<PriceHistory> PriceHistories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<QuestionAnswer> QuestionAnswers { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<ReviewAnswer> ReviewAnswers { get; set; } = null!;
    public DbSet<WishList> WishLists { get; set; } = null!;
    public DbSet<MainCategoryImage> MainCategoryImages { get; set; } = null!;
    public DbSet<SubcategoryImage> SubcategoryImages { get; set; } = null!;
    public DbSet<SubcategoryFilter> SubcategoryFilters { get; set; } = null!;
    public DbSet<ShowcaseGroup> ShowcaseGroups { get; set; } = null!;
    public DbSet<Banner> Banners { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        DbSaveChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        DbSaveChanges();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        DbSaveChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        DbSaveChanges();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void DbSaveChanges()
    {
        var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);

        foreach (var entry in addedEntries)
        {
            if (entry.Entity is not Auditable) continue;

            var defaultDate = DateTime.UtcNow;

            var createdAt = entry.Property(nameof(Auditable.CreatedAt)).CurrentValue;
            if (createdAt is null)
            {
                entry.Property(nameof(Auditable.CreatedAt)).CurrentValue = defaultDate;
            }
        }
    }
}