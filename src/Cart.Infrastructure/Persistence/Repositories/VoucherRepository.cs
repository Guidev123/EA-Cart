using Cart.Core.Entities;
using Cart.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure.Persistence.Repositories
{
    public class VoucherRepository(CartDbContext context) : IVoucherRepository
    {
        private readonly CartDbContext _context = context;

        public async Task CreateAsync(Voucher voucher) =>
            await _context.Vouchers.AddAsync(voucher);

        public void Delete(Voucher voucher) =>
            _context.Vouchers.Remove(voucher);

        public async Task<Voucher?> GetByCodeAsync(string voucherCode) =>
            await _context.Vouchers.AsNoTracking().FirstOrDefaultAsync(x => x.Code == voucherCode);

    }
}
