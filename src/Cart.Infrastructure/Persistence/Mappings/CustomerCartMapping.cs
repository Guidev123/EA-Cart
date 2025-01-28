using Cart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Infrastructure.Persistence.Mappings
{
    public class CustomerCartMapping : IEntityTypeConfiguration<CustomerCart>
    {
        public void Configure(EntityTypeBuilder<CustomerCart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt).HasColumnType("DATETIME2");
            builder.Property(x => x.Discount).HasColumnType("MONEY");

            builder.OwnsOne(x => x.Voucher, p =>
            {
                p.Property(v => v.Percentual).HasColumnName("Percentual").HasColumnType("MONEY");
                p.Property(v => v.DiscountValue).HasColumnName("DiscountValue").HasColumnType("MONEY");
                p.Property(v => v.Code).HasColumnName("Code");
                p.Property(v => v.DiscountType).HasColumnName("DiscountType");
            });

            builder.HasMany(x => x.Items).WithOne(x => x.CustomerCart).HasForeignKey(x => x.CartId);
        }
    }
}
