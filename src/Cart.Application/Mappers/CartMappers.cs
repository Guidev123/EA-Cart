using Cart.Application.UseCases.Cart.GetByCustomerId;
using Cart.Core.Entities;

namespace Cart.Application.Mappers
{
    public static class CartMappers
    {
        public static GetByCustomerIdResponse MapToResponse(this CustomerCart cart)
            => new(cart.TotalPrice, cart.Items.Select(x => x.MapFromEntity()).ToList(),
                   cart.Voucher?.Code, cart.VoucherIsUsed, cart.Discount);
    }
}
