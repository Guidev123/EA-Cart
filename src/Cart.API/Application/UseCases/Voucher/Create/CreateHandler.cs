using Cart.API.Application.Response;
using Cart.Core.Repositories;

namespace Cart.API.Application.UseCases.Voucher.Create
{
    public class CreateHandler(ICustomerCartRepository cartRepository)
               : UseCase<CreateRequest, CreateResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public override async Task<Response<CreateResponse>> HandleAsync(CreateRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
