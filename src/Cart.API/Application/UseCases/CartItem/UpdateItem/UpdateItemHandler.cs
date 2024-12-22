using Cart.API.Application.Response;
using Cart.Core.Repositories;

namespace Cart.API.Application.UseCases.CartItem.UpdateItem
{
    public class UpdateItemHandler(ICustomerCartRepository cartRepository)
               : UseCase<UpdateItemRequest, UpdateItemResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public override async Task<Response<UpdateItemResponse>> HandleAsync(UpdateItemRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
