using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Item.Remove
{
    public class RemoveItemFromCartHandler(IUnitOfWork unitOfWork)
               : Handler, IUseCase<RemoveItemFromCartRequest, RemoveItemFromCartResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<RemoveItemFromCartResponse>> HandleAsync(RemoveItemFromCartRequest input)
        {
            var customerCart = await _unitOfWork.Carts.GetByCustomerIdAsync(input.CustomerId);
            if (customerCart is null) return new(null, 404, "Cart not found");

            var cartItem = customerCart.Itens.FirstOrDefault(x => x.ProductId == input.ProductId);
            if (cartItem is null) return new(null, 404, "Item not found");

            customerCart.RemoveItem(cartItem);

            await _unitOfWork.BeginTransactionAsync();

            _unitOfWork.Carts.RemoveCartItem(cartItem);
            await _unitOfWork.CompleteAsync();

            _unitOfWork.Carts.UpdateCart(customerCart);
            await _unitOfWork.CompleteAsync();

            return await _unitOfWork.CommitAsync()
                ? new(null, 204)
                : new(null, 400, "Something has failed to save data");
        }
    }
}
