using Cart.Core.Enums;

namespace Cart.Core.ValueObjects
{
    public class Voucher : ValueObject
    {
        public Voucher(decimal? percentual, decimal? discountValue,
                       string code, EDiscountType discountType)
        {
            Percentual = percentual;
            DiscountValue = discountValue;
            Code = code;
            DiscountType = discountType;
        }

        public decimal? Percentual { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public string Code { get; private set; } = string.Empty;
        public EDiscountType DiscountType { get; private set; } = EDiscountType.Value;
    }
}
