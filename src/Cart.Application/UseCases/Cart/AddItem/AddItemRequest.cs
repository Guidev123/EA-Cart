namespace Cart.Application.UseCases.Cart.AddItem
{
    public class AddItemRequest
    {
        public AddItemRequest(Guid productId, string name, decimal price, string imageUrl, int quantity)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Quantity = quantity;
        }
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; }
        public int Quantity { get; private set; }

        public void AssociateCustomerId(Guid id) => CustomerId = id;
    }
}
