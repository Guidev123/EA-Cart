using Cart.Application.Response;
using Cart.Core.Repositories;

namespace Cart.Application.UseCases.Voucher.Remove
{
    public class RemoveVoucherHandler(IVoucherRepository voucherRepository)
               : Handler, IUseCase<RemoveVoucherRequest, RemoveVoucherResponse>
    {
        private readonly IVoucherRepository _voucherRepository = voucherRepository;

        public async Task<Response<RemoveVoucherResponse>> HandleAsync(RemoveVoucherRequest input)
        {
            var voucher = await _voucherRepository.GetByCodeAsync(input.Code);
            if (voucher is null)
                return new(null, 404, "Voucher not found");

            _voucherRepository.Delete(voucher);

            return new(null, 204);
        }
    }
}
