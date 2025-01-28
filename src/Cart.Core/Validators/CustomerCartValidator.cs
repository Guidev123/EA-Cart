using Cart.Core.Entities;
using FluentValidation;

namespace Cart.Core.Validators
{
    public class CustomerCartValidation : AbstractValidator<CustomerCart>
    {
        public CustomerCartValidation()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer not found");
            RuleFor(x => x.Items.Count).GreaterThan(0).WithMessage("Cart has no itens");
            RuleFor(x => x.TotalPrice).GreaterThan(0).WithMessage("Cart price should be greater than $0");
        }
    }
}
