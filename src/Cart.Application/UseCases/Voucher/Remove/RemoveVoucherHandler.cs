using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Voucher.Remove
{
    public class RemoveVoucherHandler(ICartRepository cartRepository)
               : Handler, IUseCase<RemoveVoucherRequest, RemoveVoucherResponse>
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public async Task<Response<RemoveVoucherResponse>> HandleAsync(RemoveVoucherRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
