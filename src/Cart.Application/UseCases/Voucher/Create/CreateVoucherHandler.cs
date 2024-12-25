using Cart.Application.Mappers;
using Cart.Application.Response;
using Cart.Core.Repositories;
using Cart.Core.Validators;

namespace Cart.Application.UseCases.Voucher.Create
{
    public class CreateVoucherHandler(IUnitOfWork unitOfWork)
               : Handler, IUseCase<CreateVoucherRequest, CreateVoucherResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<CreateVoucherResponse>> HandleAsync(CreateVoucherRequest input)
        {
            var voucher = input.MapToEntity();
            var validationResult = ValidateEntity(new VoucherValidator(), voucher);

            if (!validationResult.IsValid)
                return new(null, 400, "Error", GetAllErrors(validationResult));

            await _unitOfWork.Vouchers.CreateAsync(voucher);
            await _unitOfWork.CompleteAsync();

            return new(new(voucher.Id), 201);
        }
    }
}
