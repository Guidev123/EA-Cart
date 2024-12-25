using Cart.Application.UseCases.Voucher.Create;
using Cart.Core.Entities;

namespace Cart.Application.Mappers
{
    public static class VoucherMappers
    {
        public static Voucher MapToEntity(this CreateVoucherRequest request) =>
            new(request.Percentual, request.DiscountValue, request.Code, request.DiscountType, request.Quantity);
    }
}
