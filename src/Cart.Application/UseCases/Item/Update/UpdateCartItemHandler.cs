using Cart.Application.Response;
using Cart.Core.Entities;
using Cart.Core.Repositories;
using Cart.Core.Validators;

namespace Cart.Application.UseCases.Item.Update
{
    public class UpdateCartItemHandler(IUnitOfWork unitOfWork)
               : Handler, IUseCase<UpdateCartItemRequest, UpdateCartItemResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<UpdateCartItemResponse>> HandleAsync(UpdateCartItemRequest input)
        {
            var customerCart = await _unitOfWork.Carts.GetByCustomerIdAsync(input.CustomerId);
            if (customerCart is null) return new(null, 404, "Cart not found");

            var item = customerCart.Itens.FirstOrDefault(x => x.ProductId == input.ProductId);
            if (item is null) return new(null, 404, "Item not found");

            var itemCart = await GetItemCartValidatedAsync(customerCart, item, input.ProductId);
            if (itemCart.Data is null || !itemCart.IsSuccess) return new(null, 404, itemCart!.Message, itemCart.Errors);

            customerCart.UpdateUnities(itemCart.Data, item.Quantity);

            await _unitOfWork.BeginTransactionAsync();

            _unitOfWork.Carts.UpdateCartItem(item);
            await _unitOfWork.CompleteAsync();

            _unitOfWork.Carts.UpdateCart(customerCart);
            await _unitOfWork.CompleteAsync();

            return await _unitOfWork.CommitAsync()
                            ? new(null, 204)
                            : new(null, 400, "Something has failed to save data");
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

            var cart = await _unitOfWork.Carts.GetCartItemByIdAsync(item.CartId, productId);
            if (cart is null)
            {
                AddError(validationResult, "Item not found");
                return new(item, 400, "Error", GetAllErrors(validationResult));
            }

            return new(item, 200, "Success", GetAllErrors(validationResult));
        }
    }
}
