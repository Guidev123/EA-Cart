using Cart.Application.Response;
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

            ResponseMessage<Voucher> voucher;
            try
            {
                voucher = await _bus.RequestAsync<AppliedVoucherIntegrationEvent, ResponseMessage<Voucher>>
                    (new AppliedVoucherIntegrationEvent(input.VoucherCode));

                if (voucher.Data is null) return new(null, 404, "Voucher not found");
            }
            catch
            {
                return new(null, 400, "Fail to apply voucher");
            }

            customerCart.ApplyVoucher(voucher.Data);

            _unitOfWork.Carts.UpdateCart(customerCart);
            await _unitOfWork.CompleteAsync();

            return new(null, 204);
        }
    }
}
