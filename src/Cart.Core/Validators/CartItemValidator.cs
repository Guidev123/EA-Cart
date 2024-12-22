using Cart.Core.Entities;
using FluentValidation;

namespace Cart.Core.Validators
{
    public class CartItemValidator : AbstractValidator<CartItem>
    {
        public CartItemValidator()
        {
            RuleFor(c => c.ProductId).NotEqual(Guid.Empty).WithMessage("Product id is invalid");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Product name is invalid");
            RuleFor(c => c.Price).GreaterThan(0).WithMessage(item => $"Product {item.Name} price should be greater than $0");
            RuleFor(c => c.Quantity).GreaterThan(0).WithMessage(item => $"Product {item.Name} quantity should be greater than 0");
            RuleFor(c => c.Quantity).LessThanOrEqualTo(5)
                .WithMessage("Product quantity should be less than or equal to 5");
        }
    }
}
