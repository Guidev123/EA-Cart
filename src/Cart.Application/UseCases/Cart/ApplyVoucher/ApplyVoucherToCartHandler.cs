using Cart.Application.Response;
using Cart.Core.Repositories;
using FluentValidation.Results;

namespace Cart.Application.UseCases.Cart.ApplyVoucher
{
    public class ApplyVoucherToCartHandler(IUnitOfWork unitOfWork)
               : Handler, IUseCase<ApplyVoucherToCartRequest, ApplyVoucherToCartResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<ApplyVoucherToCartResponse>> HandleAsync(ApplyVoucherToCartRequest input)
        {
            var customerCart = await _unitOfWork.Carts.GetByCustomerIdAsync(input.CustomerId);
            if (customerCart is null) return new(null, 404, "Cart not found");

            var voucher = await _unitOfWork.Vouchers.GetByCodeAsync(input.VoucherCode);
            if (voucher is null)
                return new(null, 404, "Voucher not found");

            customerCart.ApplyVoucher(voucher);

            _unitOfWork.Carts.UpdateCart(customerCart);
            await _unitOfWork.CompleteAsync();

            return new(null, 204);
        }
    }
}
