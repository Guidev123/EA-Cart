using Cart.Core.Entities;
using Cart.Core.Repositories;

namespace Cart.Infrastructure.Persistence.Repositories
{
    public class VoucherRepository(CartDbContext context) : IVoucherRepository
    {
        private readonly CartDbContext _context = context;
        public Task<Voucher?> GetVoucherByCodeAsync(string voucherCode)
        {
            throw new NotImplementedException();
        }
    }
}
