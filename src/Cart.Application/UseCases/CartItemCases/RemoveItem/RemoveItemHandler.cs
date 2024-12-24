using Cart.Application.Response;
using Cart.Application.UseCases;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.CartItemCases.RemoveItem
{
    public class RemoveItemHandler(ICustomerCartRepository cartRepository)
               : Handler, IUseCase<RemoveItemRequest, RemoveItemResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public async Task<Response<RemoveItemResponse>> HandleAsync(RemoveItemRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
