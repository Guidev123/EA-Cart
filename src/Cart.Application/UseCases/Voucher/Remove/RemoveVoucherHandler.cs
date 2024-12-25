using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Voucher.Remove
{
    public class RemoveVoucherHandler(IUnitOfWork unitOfWork)
               : Handler, IUseCase<RemoveVoucherRequest, RemoveVoucherResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<RemoveVoucherResponse>> HandleAsync(RemoveVoucherRequest input)
        {
            var voucher = await _unitOfWork.Vouchers.GetByCodeAsync(input.Code);
            if (voucher is null)
                return new(null, 404, "Voucher not found");

            _unitOfWork.Vouchers.Delete(voucher);
            await _unitOfWork.CompleteAsync();

            return new(null, 204);
        }
    }
}
