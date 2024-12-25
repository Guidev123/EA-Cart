using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Cart.ApplyVoucher
{
    public class ApplyVoucherToCartHandler(ICartRepository cartRepository,
                                           IVoucherRepository voucherRepository)
               : Handler, IUseCase<ApplyVoucherToCartRequest, ApplyVoucherToCartResponse>
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IVoucherRepository _voucherRepository = voucherRepository;
        public async Task<Response<ApplyVoucherToCartResponse>> HandleAsync(ApplyVoucherToCartRequest input)
        {
            var customerCart = await _cartRepository.GetByCustomerIdAsync(input.CustomerId);
            if (customerCart is null) return new(null, 404, "Cart not found");

            var voucher = await _voucherRepository.GetByCodeAsync(input.VoucherCode);
            if(voucher is null)
                return new(null, 404, "Voucher not found");

            customerCart.ApplyVoucher(voucher);
            _voucherRepository.Delete(voucher);

            _cartRepository.UpdateCart(customerCart);

            return new(null, 204);
        }
    }
}
