using Cart.Core.Enums;

namespace Cart.Application.UseCases.Voucher.Create
{
    public record CreateVoucherRequest(decimal? Percentual, decimal? DiscountValue, string Code, EDiscountType DiscountType);
}
