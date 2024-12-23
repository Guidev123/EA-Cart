using Cart.API.Application.Response;
using Cart.Core.Repositories;

namespace Cart.API.Application.UseCases.CustomerCart.ApplyVoucher
{
    public class ApplyVoucherHandler(ICustomerCartRepository cartRepository)
               : Handler, IUseCase<ApplyVoucherRequest, ApplyVoucherResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public async Task<Response<ApplyVoucherResponse>> HandleAsync(ApplyVoucherRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
