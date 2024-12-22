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

        public Guid ProductId { get; }
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public Guid CartId { get; private set; }
        public CustomerCart CustomerCart { get; private set; } = null!;
        internal void AssociateCart(Guid cartId) => CartId = cartId;
        internal decimal CalculateValue() => Quantity * Price;
        internal void AddUnity(int quantity) => Quantity += quantity;
        internal void UpdateUnities(int quantity) => Quantity = quantity;
    }
}
