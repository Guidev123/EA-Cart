using Cart.API.Application.Response;
using Cart.Core.Repositories;

namespace Cart.API.Application.UseCases.CustomerCart.HandleExistent
{
    public class HandleExistentHandler(ICustomerCartRepository cartRepository)
               : Handler, IUseCase<HandleExistentRequest, HandleExistentResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public async Task<Response<HandleExistentResponse>> HandleAsync(HandleExistentRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
