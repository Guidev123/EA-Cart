namespace Cart.Application.UseCases.Cart.ApplyVoucher
{
    public record ApplyVoucherToCartRequest(string VoucherCode, Guid CustomerId);
}
