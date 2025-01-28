using Cart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Infrastructure.Persistence.Mappings
{
    public class CartItemMapping : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Price).HasColumnType("MONEY").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnType("DATETIME2").IsRequired();
        }
    }
}
