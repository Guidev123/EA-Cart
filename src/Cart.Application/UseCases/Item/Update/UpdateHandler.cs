using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Item.Update
{
    public class UpdateHandler(ICustomerCartRepository cartRepository)
               : Handler, IUseCase<UpdateRequest, UpdateResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public async Task<Response<UpdateResponse>> HandleAsync(UpdateRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
