using Cart.Application.Mappers;
using Cart.Application.Response;
using Cart.Core.Entities;
using Cart.Core.Repositories;
using Cart.Core.Validators;
using FluentValidation.Results;

namespace Cart.Application.UseCases.Cart.AddItem
{
    public sealed class AddItemToCartHandler(IUnitOfWork unitOfWork)
               : Handler, IUseCase<AddItemToCartRequest, AddItemToCartResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<AddItemToCartResponse>> HandleAsync(AddItemToCartRequest input)
        {
            try
            {
                var customerCart = await _unitOfWork.Carts.GetByCustomerIdAsync(input.CustomerId!.Value);
                var cartItem = input.MapToEntity();

                await _unitOfWork.BeginTransactionAsync();

                if (customerCart is null)
                    return await HandleNewAsync(input.CustomerId.Value, cartItem);

                var validationResult = ValidateEntity(new CartItemValidator(), cartItem);

                if (!validationResult.IsValid)
                    return new(null, 400, "Error");

                customerCart.AddItem(cartItem);

                if (await _unitOfWork.Carts.CartItemAlreadyExists(cartItem.ProductId))
                {
                    _unitOfWork.Carts.UpdateCartItem(cartItem);
                    await _unitOfWork.CompleteAsync();
                }
                else
                {
                    await _unitOfWork.Carts.AddCartItem(cartItem);
                    await _unitOfWork.CompleteAsync();
                }

                _unitOfWork.Carts.UpdateCart(customerCart);
                await _unitOfWork.CompleteAsync();

                return await _unitOfWork.CommitAsync()
                    ? new(new AddItemToCartResponse(customerCart.Id), 201)
                    : new(null, 400, "Something has failed to persist data");
            }
            catch(Exception ex)
            {
                if (_unitOfWork.HasActiveTransaction())
                    await _unitOfWork.RollbackTransactionAsync();

                var validationResult = new ValidationResult();
                AddError(validationResult, ex.Message);

                return new(null, 500, "Something has failed to persist data");
            }
        }

        private async Task<Response<AddItemToCartResponse>> HandleNewAsync(Guid customerId, CartItem cartItem)
        {
            var validationResult = ValidateEntity(new CartItemValidator(), cartItem);

            if (!validationResult.IsValid) return new(null, 400, "Error");

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
