using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Item.Remove
{
    public class RemoveItemFromCartHandler(ICartRepository cartRepository)
               : Handler, IUseCase<RemoveItemFromCartRequest, RemoveItemFromCartResponse>
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        public async Task<Response<RemoveItemFromCartResponse>> HandleAsync(RemoveItemFromCartRequest input)
        {
            var customerCart = await _cartRepository.GetByCustomerIdAsync(input.CustomerId);
            if (customerCart is null) return new(null, 404, "Cart not found");

            var cartItem = customerCart.Itens.FirstOrDefault(x => x.ProductId == input.ProductId);
            if (cartItem is null) return new(null, 404, "Item not found");

            customerCart.RemoveItem(cartItem);

            _cartRepository.RemoveCartItem(cartItem);
            _cartRepository.UpdateCart(customerCart);

            return new(null, 204);
        }
    }
}
