namespace Cart.Application.UseCases.Item.Remove
{
    public record RemoveItemFromCartRequest(Guid ProductId, Guid CustomerId);
}
