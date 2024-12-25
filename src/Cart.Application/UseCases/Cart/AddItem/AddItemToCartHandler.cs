using Cart.Application.Mappers;
using Cart.Application.Response;
using Cart.Core.Entities;
using Cart.Core.Repositories;
using Cart.Core.Validators;

namespace Cart.Application.UseCases.Cart.AddItem
{
    public class AddItemToCartHandler(IUnitOfWork unitOfWork)
               : Handler, IUseCase<AddItemToCartRequest, AddItemToCartResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<AddItemToCartResponse>> HandleAsync(AddItemToCartRequest input)
        {
            var customerCart = await _unitOfWork.Carts.GetByCustomerIdAsync(input.CustomerId!.Value);
            var cartItem = input.MapToEntity();

            await _unitOfWork.BeginTransactionAsync();

            if (customerCart is null)
                return await HandleNewAsync(input.CustomerId.Value, cartItem);

            var existentProduct = await _unitOfWork.Carts.CartItemAlreadyExists(cartItem.Id);

            var validationResult = ValidateEntity(new CartItemValidator(), cartItem);

            if (!validationResult.IsValid)
                return new(null, 400, "Error", GetAllErrors(validationResult));

            customerCart.AddItem(cartItem);

            if (existentProduct) _unitOfWork.Carts.UpdateCartItem(cartItem);
            else await _unitOfWork.Carts.AddCartItem(cartItem);

            _unitOfWork.Carts.UpdateCart(customerCart);

            await _unitOfWork.CompleteAsync();

            return await _unitOfWork.CommitAsync()
                ? new(new AddItemToCartResponse(customerCart.Id), 201)
                : new(null, 400, "Something has failed to save data");
        }

        private async Task<Response<AddItemToCartResponse>> HandleNewAsync(Guid customerId, CartItem cartItem)
        {
            var validationResult = ValidateEntity(new CartItemValidator(), cartItem);

            if (!validationResult.IsValid) return new(null, 400, "Error", GetAllErrors(validationResult));

            var customerCart = new CustomerCart(customerId);

            customerCart.AddItem(cartItem);
            await _unitOfWork.Carts.CreateAsync(customerCart);
            await _unitOfWork.CompleteAsync();

            return await _unitOfWork.CommitAsync()
                ? new(new AddItemToCartResponse(customerCart.Id), 201)
                : new(null, 400, "Something has failed to save data");
        }
    }
}
