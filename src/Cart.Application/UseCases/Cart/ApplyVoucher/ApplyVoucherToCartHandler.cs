using Cart.Application.Response;
using Cart.Core.Enums;
using Cart.Core.Repositories;
using Cart.Core.ValueObjects;
using SharedLib.Domain.Messages.Integration;
using SharedLib.Domain.Messages.Integration.AppliedVoucher;
using SharedLib.MessageBus;

namespace Cart.Application.UseCases.Cart.ApplyVoucher
{
    public class ApplyVoucherToCartHandler(IUnitOfWork unitOfWork,
                                           IMessageBus bus)
               : Handler, IUseCase<ApplyVoucherToCartRequest, ApplyVoucherToCartResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMessageBus _bus = bus;
        public async Task<Response<ApplyVoucherToCartResponse>> HandleAsync(ApplyVoucherToCartRequest input)
        {
            var customerCart = await _unitOfWork.Carts.GetByCustomerIdAsync(input.CustomerId);
            if (customerCart is null) return new(null, 404, "Cart not found");

            ResponseMessage<AppliedVoucherResponse> voucherResponse;
            try
            {
                voucherResponse = await _bus.RequestAsync<AppliedVoucherIntegrationEvent, ResponseMessage<AppliedVoucherResponse>>
                    (new AppliedVoucherIntegrationEvent(input.VoucherCode));

                if (voucherResponse.Data is null) return new(null, 404, "Voucher not found");
            }
            catch
            {
                return new(null, 400, "Fail to apply voucher");
            }

            var voucher = new Voucher(voucherResponse.Data.Percentual,
                                      voucherResponse.Data.DiscountValue,
                                      voucherResponse.Data.Code,
                                      (EDiscountType)voucherResponse.Data.DiscountType);

            customerCart.ApplyVoucher(voucher);

            _unitOfWork.Carts.UpdateCart(customerCart);
            await _unitOfWork.CompleteAsync();

            return new(null, 204);
        }
    }
}
