using System.Text.Json.Serialization;

namespace Cart.Application.UseCases.Item.Update
{
    public class UpdateCartItemRequest
    {
        public UpdateCartItemRequest(Guid productId, string name, decimal price, string imageUrl, int quantity, Guid customerId)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Quantity = quantity;
            CustomerId = customerId;
        }
        public Guid CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }
        [JsonIgnore]
        public Guid ProductId { get; set; }
    }
}
