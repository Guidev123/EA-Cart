using Cart.Core.Entities;
using Cart.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cart.Infrastructure.Persistence
{
    public class CartDbContext(DbContextOptions<CartDbContext> options,
                               IOptions<ContextSettings> contextSettings)
                             : DbContext(options)
    {
        private readonly ContextSettings _contextSettings = contextSettings.Value;
        public DbSet<CustomerCart> CustomerCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_contextSettings.ConnectionString)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartDbContext).Assembly);

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e =>
                e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("VARCHAR(160)");

            foreach (var rel in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
                rel.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }
}
