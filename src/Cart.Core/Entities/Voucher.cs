﻿using Cart.Core.DomainObjects;
using Cart.Core.Enums;

namespace Cart.Core.Entities
{
    public class Voucher : Entity
    {
        public Voucher(decimal? percentual, decimal? discountValue, string code, EDiscountType discountType)
        {
            Percentual = percentual;
            DiscountValue = discountValue;
            Code = code;
            DiscountType = discountType;
        }
        protected Voucher() { }

        public decimal? Percentual { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public string Code { get; private set; } = string.Empty;
        public EDiscountType DiscountType { get; private set; } = EDiscountType.Value;
    }
}