using Cart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Infrastructure.Persistence.Mappings
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Vouchers");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt).HasColumnType("DATETIME2");
            builder.Property(x => x.DiscountValue).HasColumnType("MONEY");
            builder.Property(x => x.Percentual).HasColumnType("MONEY");
        }
    }
}
