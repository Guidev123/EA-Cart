using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.CartItemCases.UpdateItem
{
    public class UpdateItemHandler(ICustomerCartRepository cartRepository)
               : Handler, IUseCase<UpdateItemRequest, UpdateItemResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public async Task<Response<UpdateItemResponse>> HandleAsync(UpdateItemRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
