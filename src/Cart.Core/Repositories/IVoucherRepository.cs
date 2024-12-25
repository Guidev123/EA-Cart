using Cart.Core.Entities;

namespace Cart.Core.Repositories
{
    public interface IVoucherRepository
    {
        Task<Voucher?> GetVoucherByCodeAsync(string voucherCode);
    }
}
