namespace Cart.Application.UseCases.Item.Update
{
    public class UpdateCartItemRequest
    {
        public UpdateCartItemRequest(int quantity, Guid productId, Guid customerId)
        {
            ProductId = productId;
            Quantity = quantity;
            CustomerId = customerId;
        }
        public int Quantity { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}
