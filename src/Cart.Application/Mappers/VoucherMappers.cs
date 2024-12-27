using Cart.Application.DTOs;
using Cart.Core.ValueObjects;

namespace Cart.Application.Mappers
{
    public static class VoucherMappers
    {
        public static Voucher MapFromResponseToVoucher(this VoucherDTO response) =>
            new(response.Percentual, response.DiscountValue, response.Code, response.DiscountType);
    }
}
