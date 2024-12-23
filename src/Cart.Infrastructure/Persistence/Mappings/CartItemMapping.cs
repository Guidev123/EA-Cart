using Cart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Infrastructure.Persistence.Mappings
{
    public class CartItemMapping : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItens");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price).HasColumnType("MONEY");
            builder.Property(x => x.CreatedAt).HasColumnType("DATETIME2");
        }
    }
}
