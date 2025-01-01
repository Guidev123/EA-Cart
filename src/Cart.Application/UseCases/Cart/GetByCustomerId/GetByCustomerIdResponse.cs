using Cart.Application.DTOs;

namespace Cart.Application.UseCases.Cart.GetByCustomerId
{
    public record GetByCustomerIdResponse(decimal TotalPrice, List<CartItemDTO> CartItens,
                                          string? VoucherCode, bool VoucherIsUsed, decimal? Discount);
}
