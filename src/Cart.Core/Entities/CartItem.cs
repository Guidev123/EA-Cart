using Cart.Core.DomainObjects;

namespace Cart.Core.Entities
{
    public class CartItem : Entity
    {
        public CartItem(Guid productId, string name, decimal price, string imageUrl, int quantity)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Quantity = quantity;
        }
        protected CartItem() { }

        public Guid ProductId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public Guid CartId { get; private set; }
        public CustomerCart CustomerCart { get; private set; } = null!;
        public void AssociateCart(Guid cartId) => CartId = cartId;
        public decimal CalculateValue() => Quantity * Price;
        public void AddUnity(int quantity) => Quantity += quantity;
        public void UpdateUnities(int quantity) => Quantity = quantity;
    }
}
