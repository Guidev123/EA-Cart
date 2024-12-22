using Cart.Core.Entities;
using FluentValidation;

namespace Cart.Core.Validators
{
    public class VoucherValidator : AbstractValidator<Voucher>
    {
        public VoucherValidator()
        {
            RuleFor(x => x.Code).NotNull().NotEmpty().WithMessage("Code should not be empty");
        }
    }
}
