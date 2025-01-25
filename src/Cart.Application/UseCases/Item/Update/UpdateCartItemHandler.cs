using Cart.Application.Response;
using Cart.Core.Repositories;
using FluentValidation.Results;

namespace Cart.Application.UseCases.Item.Update
{
    public sealed class UpdateCartItemHandler(IUnitOfWork unitOfWork)
               : Handler, IUseCase<UpdateCartItemRequest, UpdateCartItemResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<UpdateCartItemResponse>> HandleAsync(UpdateCartItemRequest input)
        {
            try
            {
                var customerCart = await _unitOfWork.Carts.GetByCustomerIdAsync(input.CustomerId);
                if (customerCart is null) return new(false, 404, null, "Cart not found");

                var item = customerCart.Itens.FirstOrDefault(x => x.ProductId == input.ProductId);
                if (item is null) return new(false, 404, null, "Item not found");

                customerCart.UpdateUnities(item, input.Quantity);

                await _unitOfWork.BeginTransactionAsync();

                _unitOfWork.Carts.UpdateCartItem(item);
                await _unitOfWork.CompleteAsync();

                _unitOfWork.Carts.UpdateCart(customerCart);
                await _unitOfWork.CompleteAsync();

                return await _unitOfWork.CommitAsync()
                                ? new(true, 204, null)
                                : new(false, 400, null, "Something has failed to save data");
            }
            catch (Exception ex)
            {
                if (_unitOfWork.HasActiveTransaction())
                    await _unitOfWork.RollbackTransactionAsync();

                var validationResult = new ValidationResult();
                AddError(validationResult, ex.Message);

                return new(false, 500, null, "Something has failed to persist data", GetAllErrors(validationResult));
            }
        }
    }
}
