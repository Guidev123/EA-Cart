using Cart.Application.Mappers;
using Cart.Application.Response;
using Cart.Core.Entities;
using Cart.Core.Repositories;
using Cart.Core.Validators;

namespace Cart.Application.UseCases.Cart.AddItem
{
    public class AddItemToCartHandler(ICartRepository cartRepository)
               : Handler, IUseCase<AddItemToCartRequest, AddItemToCartResponse>
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        public async Task<Response<AddItemToCartResponse>> HandleAsync(AddItemToCartRequest input)
        {
            var customerCart = await _cartRepository.GetByCustomerIdAsync(input.CustomerId!.Value);
            var cartItem = input.MapToEntity();

            if (customerCart is null) return await HandleNewAsync(input.CustomerId.Value, cartItem);

            var existentProduct = await _cartRepository.CartItemAlreadyExists(cartItem.Id);

            var validationResult = ValidateEntity(new CartItemValidator(), cartItem);

            if (!validationResult.IsValid) return new(null, 400, "Error", GetAllErrors(validationResult));

            customerCart.AddItem(cartItem);

            if (existentProduct) _cartRepository.UpdateCartItem(cartItem);
            else await _cartRepository.AddCartItem(cartItem);

            _cartRepository.UpdateCart(customerCart);

            return new(new AddItemToCartResponse(customerCart.Id), 201);
        }

        private async Task<Response<AddItemToCartResponse>> HandleNewAsync(Guid customerId, CartItem cartItem)
        {
            var validationResult = ValidateEntity(new CartItemValidator(), cartItem);

            if (!validationResult.IsValid) return new(null, 400, "Error", GetAllErrors(validationResult));

            var customerCart = new CustomerCart(customerId);

            customerCart.AddItem(cartItem);
            await _cartRepository.CreateAsync(customerCart);
            return new(new AddItemToCartResponse(customerCart.Id), 201);
        }
    }
}
