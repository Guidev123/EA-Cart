using Cart.Application.Interfaces.ExternalServices;
using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Cart.ApplyVoucher
{
    public class ApplyVoucherToCartHandler(IUnitOfWork unitOfWork, IVoucherRestService voucherRestService)
               : Handler, IUseCase<ApplyVoucherToCartRequest, ApplyVoucherToCartResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IVoucherRestService _voucherRestService = voucherRestService;
        public async Task<Response<ApplyVoucherToCartResponse>> HandleAsync(ApplyVoucherToCartRequest input)
        {
            var customerCart = await _unitOfWork.Carts.GetByCustomerIdAsync(input.CustomerId);
            if (customerCart is null) return new(null, 404, "Cart not found");

            var voucherResult = await _voucherRestService.GetVoucherByCodeAsync(input.VoucherCode);
            if(voucherResult.Data is null || !voucherResult.IsSuccess) return new(null, voucherResult.Code, voucherResult.Message);

            customerCart.ApplyVoucher(voucherResult.Data);

            _unitOfWork.Carts.UpdateCart(customerCart);
            await _unitOfWork.CompleteAsync();

            return new(null, 204);
        }
    }
}
