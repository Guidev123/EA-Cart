using Cart.API.Application.Response;
using Cart.Core.Repositories;

namespace Cart.API.Application.UseCases.CustomerCart.HandleNew
{
    public class HandleNewHandler(ICustomerCartRepository cartRepository)
               : Handler, IUseCase<HandleNewRequest, HandleNewResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public async Task<Response<HandleNewResponse>> HandleAsync(HandleNewRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
