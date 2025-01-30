using Cart.Application.DTOs;

namespace Cart.Application.UseCases.Cart.GetByCustomerId
{
    public record GetByCustomerIdResponse(decimal TotalPrice, List<CartItemDTO> CartItems,
                                          string? VoucherCode, bool VoucherIsUsed, decimal? Discount);
}
