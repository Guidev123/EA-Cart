using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Item.Remove
{
    public class RemoveHandler(ICustomerCartRepository cartRepository)
               : Handler, IUseCase<RemoveRequest, RemoveResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public async Task<Response<RemoveResponse>> HandleAsync(RemoveRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
