using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Voucher.Create
{
    public class CreateVoucherHandler(ICartRepository cartRepository)
               : Handler, IUseCase<CreateVoucherRequest, CreateVoucherResponse>
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        public async Task<Response<CreateVoucherResponse>> HandleAsync(CreateVoucherRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
