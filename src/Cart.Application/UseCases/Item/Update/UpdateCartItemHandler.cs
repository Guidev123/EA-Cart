using Cart.Application.Response;
using Cart.Core.Entities;
using Cart.Core.Repositories;
using Cart.Core.Validators;

namespace Cart.Application.UseCases.Item.Update
{
    public class UpdateCartItemHandler(ICartRepository cartRepository)
               : Handler, IUseCase<UpdateCartItemRequest, UpdateCartItemResponse>
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        public async Task<Response<UpdateCartItemResponse>> HandleAsync(UpdateCartItemRequest input)
        {
            var customerCart = await _cartRepository.GetByCustomerIdAsync(input.CustomerId);
            if (customerCart is null) return new(null, 404, "Cart not found");

            var item = customerCart.Itens.FirstOrDefault(x => x.ProductId == input.ProductId);
            if (item is null) return new(null, 404, "Item not found");

            var itemCart = await GetItemCartValidatedAsync(customerCart, item, input.ProductId);
            if (itemCart.Data is null || !itemCart.IsSuccess) return new(null, 404, itemCart!.Message, itemCart.Errors);

            customerCart.UpdateUnities(itemCart.Data, item.Quantity);

            _cartRepository.UpdateCartItem(item);
            _cartRepository.UpdateCart(customerCart);

            return new(null, 204);
        }

        private async Task<Response<CartItem>> GetItemCartValidatedAsync(CustomerCart customerCart,
                                                                         CartItem item,
                                                                         Guid productId)
        {
            var validationResult = ValidateEntity(new CartItemValidator(), item);

            if (!validationResult.IsValid)
                return new(item, 400, "Error", GetAllErrors(validationResult));

            if (productId != item.ProductId)
            {
                AddError(validationResult, "Item does not correspond to what is stated");
                return new(item, 400, "Error", GetAllErrors(validationResult));
            }

            if (customerCart is null)
            {
                AddError(validationResult, "Customer cart can not be empty");
                return new(item, 400, "Error", GetAllErrors(validationResult));
            }

            var cart = await _cartRepository.GetCartItemByIdAsync(item.CartId, productId);
            if (cart is null)
            {
                AddError(validationResult, "Item not found");
                return new(item, 400, "Error", GetAllErrors(validationResult));
            }

            return new(item, 200, "Success", GetAllErrors(validationResult));
        }
    }
}
