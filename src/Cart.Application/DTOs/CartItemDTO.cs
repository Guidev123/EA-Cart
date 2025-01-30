namespace Cart.Application.DTOs
{
    public record CartItemDTO(Guid ProductId, string Name, decimal Price, string ImageUrl, int Quantity);
}
