using Cart.API.Application.Response;
using Cart.Core.Repositories;

namespace Cart.API.Application.UseCases.VoucherCases.Create
{
    public class CreateHandler(ICustomerCartRepository cartRepository)
               : Handler, IUseCase<CreateRequest, CreateResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public async Task<Response<CreateResponse>> HandleAsync(CreateRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
