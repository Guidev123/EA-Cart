using Cart.Application.Response;
using Cart.Application.UseCases;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.VoucherCases.Create
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
