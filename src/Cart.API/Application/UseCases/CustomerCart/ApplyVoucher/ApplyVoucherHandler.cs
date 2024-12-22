using Cart.API.Application.Response;
using Cart.Core.Repositories;

namespace Cart.API.Application.UseCases.CustomerCart.ApplyVoucher
{
    public class ApplyVoucherHandler(ICustomerCartRepository cartRepository)
               : UseCase<ApplyVoucherRequest, ApplyVoucherResponse>
    {
        private readonly ICustomerCartRepository _cartRepository = cartRepository;
        public override async Task<Response<ApplyVoucherResponse>> HandleAsync(ApplyVoucherRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
