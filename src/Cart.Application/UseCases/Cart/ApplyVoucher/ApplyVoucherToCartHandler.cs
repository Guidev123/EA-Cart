using Cart.Application.Response;
using Cart.Application.Services.AuthServices;
using Cart.Application.Services.ExternalServices;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Cart.ApplyVoucher
{
    public sealed class ApplyVoucherToCartHandler(IUnitOfWork unitOfWork,
                                                  IVoucherRestService voucherRestService,
                                                  IUserService userService)
               : Handler, IUseCase<ApplyVoucherToCartRequest, ApplyVoucherToCartResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserService _userService = userService;
        private readonly IVoucherRestService _voucherRestService = voucherRestService;
        public async Task<Response<ApplyVoucherToCartResponse>> HandleAsync(ApplyVoucherToCartRequest input)
        {
            var userId = await _userService.GetUserIdAsync();
            if (userId is null) return new(null, 404);

            var customerCart = await _unitOfWork.Carts.GetByCustomerIdAsync(userId.Value);
            if (customerCart is null)
                return new(null, 404, "Cart not found");

            var voucherResult = await _voucherRestService.GetVoucherByCodeAsync(input.VoucherCode);
            if(voucherResult.Data is null || !voucherResult.IsSuccess)
                return new(null, voucherResult.StatusCode, voucherResult.Message);

            customerCart.ApplyVoucher(voucherResult.Data);

            _unitOfWork.Carts.UpdateCart(customerCart);
            await _unitOfWork.CompleteAsync();

            return new(null, 204);
        }
    }
}
