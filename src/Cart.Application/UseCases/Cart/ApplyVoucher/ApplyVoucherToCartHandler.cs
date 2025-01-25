using Cart.Application.Response;
using Cart.Application.Services.ExternalServices;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Cart.ApplyVoucher
{
    public sealed class ApplyVoucherToCartHandler(IUnitOfWork unitOfWork, IVoucherRestService voucherRestService)
               : Handler, IUseCase<ApplyVoucherToCartRequest, ApplyVoucherToCartResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IVoucherRestService _voucherRestService = voucherRestService;
        public async Task<Response<ApplyVoucherToCartResponse>> HandleAsync(ApplyVoucherToCartRequest input)
        {
            var customerCart = await _unitOfWork.Carts.GetByCustomerIdAsync(input.CustomerId);
            if (customerCart is null)
                return new(false, 404, null, "Cart not found");

            var voucherResult = await _voucherRestService.GetVoucherByCodeAsync(input.VoucherCode);
            if(voucherResult.Data is null || !voucherResult.IsSuccess)
                return new(voucherResult.IsSuccess, voucherResult.Code, null, voucherResult.Message);

            customerCart.ApplyVoucher(voucherResult.Data);

            _unitOfWork.Carts.UpdateCart(customerCart);
            await _unitOfWork.CompleteAsync();

            return new(true, 204, null);
        }
    }
}
