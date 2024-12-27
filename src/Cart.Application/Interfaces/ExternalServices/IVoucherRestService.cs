using Cart.Application.Response;
using Cart.Core.ValueObjects;

namespace Cart.Application.Interfaces.ExternalServices
{
    public interface IVoucherRestService
    {
        Task<Response<Voucher>> GetVoucherByCodeAsync(string code);
    }
}
