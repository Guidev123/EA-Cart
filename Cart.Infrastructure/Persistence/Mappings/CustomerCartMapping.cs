using Cart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Infrastructure.Persistence.Mappings
{
    public class CustomerCartMapping : IEntityTypeConfiguration<CustomerCart>
    {
        public void Configure(EntityTypeBuilder<CustomerCart> builder)
        {
            builder.ToTable("CustomerCarts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt).HasColumnType("DATETIME2");
            builder.Property(x => x.Discount).HasColumnType("MONEY");

            builder.HasOne(x => x.Voucher).WithMany();
            builder.HasMany(x => x.Itens).WithOne(x => x.CustomerCart).HasForeignKey(x => x.CartId);
        }
    }
}
