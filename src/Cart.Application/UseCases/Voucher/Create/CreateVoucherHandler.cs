using Cart.Application.Mappers;
using Cart.Application.Response;
using Cart.Core.Repositories;
using Cart.Core.Validators;

namespace Cart.Application.UseCases.Voucher.Create
{
    public class CreateVoucherHandler(IVoucherRepository voucherRepository)
               : Handler, IUseCase<CreateVoucherRequest, CreateVoucherResponse>
    {
        private readonly IVoucherRepository _voucherRepository = voucherRepository;
        public async Task<Response<CreateVoucherResponse>> HandleAsync(CreateVoucherRequest input)
        {
            var voucher = input.MapToEntity();
            var validationResult = ValidateEntity(new VoucherValidator(), voucher);

            if (!validationResult.IsValid)
                return new(null, 400, "Error", GetAllErrors(validationResult));

            await _voucherRepository.CreateAsync(voucher);

            return new(new(voucher.Id), 201);
        }
    }
}
