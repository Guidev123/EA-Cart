using Cart.Application.Mappers;
using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Cart.GetByCustomerId
{
    public sealed class GetByCustomerIdHandler(IUnitOfWork unitOfWork)
                      : IUseCase<GetByCustomerIdRequest, GetByCustomerIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<GetByCustomerIdResponse>> HandleAsync(GetByCustomerIdRequest input)
        {
            var cart = await _unitOfWork.Carts.GetByCustomerIdAsync(input.CustomerId);
            if (cart is null) return new(false, 404, null, "Cart not found");

            return new(true, 200, cart.MapToResponse());
        }
    }
}
