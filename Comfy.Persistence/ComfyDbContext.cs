using Comfy.Application.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Comfy.Domain
{
    public class ComfyDbContext : IdentityDbContext<User, ApplicationRole, Guid>, IComfyDbContext
    {
        public ComfyDbContext(DbContextOptions<ComfyDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<UserLog> UserLogs { get; set; } = null!;
        public DbSet<LoggingAction> LoggingActions { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<AddressType> AddressTypes { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; } = null!;
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
        public DbSet<OrderedProduct> OrderedProducts { get; set; } = null!;
        public DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public DbSet<PriceHistory> PriceHistories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Wishlist> WhishLists { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}


