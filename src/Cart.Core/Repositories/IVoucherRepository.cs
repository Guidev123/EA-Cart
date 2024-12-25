using Cart.Core.Entities;

namespace Cart.Core.Repositories
{
    public interface IVoucherRepository
    {
        Task CreateAsync(Voucher voucher);
        void Delete(Voucher voucher);
        Task<Voucher?> GetByCodeAsync(string voucherCode);
    }
}
