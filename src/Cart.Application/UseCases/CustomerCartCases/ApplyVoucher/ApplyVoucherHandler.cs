using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.CustomerCartCases.ApplyVoucher
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
