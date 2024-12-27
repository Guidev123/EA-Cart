using Cart.Core.Enums;

namespace Cart.Application.DTOs
{
    public class VoucherDTO
    {
        public decimal? Percentual { get; set; }
        public decimal? DiscountValue { get; set; }
        public string Code { get; set; } = string.Empty;
        public EDiscountType DiscountType { get; set; }
    }
}
