using Cart.API.Application.Response;
using Cart.Core.Repositories;

namespace Cart.API.Application.UseCases.CartItem.RemoveItem
{
    public class RemoveItemHandler(ICustomerCartRepository cartRepository)
               : UseCase<RemoveItemRequest, RemoveItemResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public override async Task<Response<RemoveItemResponse>> HandleAsync(RemoveItemRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
