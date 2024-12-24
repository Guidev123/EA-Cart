using Cart.Application.Mappers;
using Cart.Application.Response;
using Cart.Core.Entities;
using Cart.Core.Repositories;
using Cart.Core.Validators;

namespace Cart.Application.UseCases.Cart.AddItem
{
    public class AddItemHandler(ICustomerCartRepository cartRepository)
               : Handler, IUseCase<AddItemRequest, AddItemResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public async Task<Response<AddItemResponse>> HandleAsync(AddItemRequest input)
        {
            var customerCart = await _cartRepository.GetByCustomerIdAsync(input.CustomerId);
            var cartItem = input.MapToEntity();

            if (customerCart is null) return await HandleNewAsync(input.CustomerId, cartItem);

            var existentProduct = await _cartRepository.CartItemAlreadyExists(cartItem);

            var validationResult = ValidateEntity(new CartItemValidator(), cartItem);

            if (!validationResult.IsValid) return new(null, 400, "Error", GetAllErrors(validationResult));

            customerCart.AddItem(cartItem);

            if (existentProduct) _cartRepository.UpdateCartItem(cartItem);
            else await _cartRepository.AddCartItem(cartItem);

            _cartRepository.UpdateCart(customerCart);

            return new(new AddItemResponse(customerCart.Id), 201);
        }

        private async Task<Response<AddItemResponse>> HandleNewAsync(Guid customerId, CartItem cartItem)
        {
            var validationResult = ValidateEntity(new CartItemValidator(), cartItem);

            if (!validationResult.IsValid) return new(null, 400, "Error", GetAllErrors(validationResult));

            var customerCart = new CustomerCart(customerId);

            customerCart.AddItem(cartItem);
            await _cartRepository.CreateAsync(customerCart);
            return new(new AddItemResponse(customerCart.Id), 201);
        }
    }
}
