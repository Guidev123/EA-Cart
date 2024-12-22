using Cart.API.Application.Response;
using Cart.Core.Repositories;

namespace Cart.API.Application.UseCases.Voucher.Remove
{
    public class RemoveHandler(ICustomerCartRepository cartRepository)
               : IUseCase<RemoveRequest, RemoveResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public async Task<Response<RemoveResponse>> HandleAsync(RemoveRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
