using Cart.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure.Persistence
{
    public class CartDbContext(DbContextOptions<CartDbContext> options)
                             : DbContext(options)
    {
        public DbSet<CustomerCart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

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
