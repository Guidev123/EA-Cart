using Cart.Core.Entities;

namespace Cart.Core.Repositories
{
    public interface ICartRepository
    {
        Task CreateAsync(CustomerCart cart);
        void UpdateCart(CustomerCart cart);
        Task<CustomerCart> GetByCustomerIdAsync(Guid id);

        Task<bool> CartItemAlreadyExists(CartItem item);
        Task<CartItem?> GetCartItemByIdAsync(Guid cartId, Guid productId);
        void UpdateCartItem(CartItem item);
        Task AddCartItem(CartItem item);
        void RemoveCartItem(CartItem item);
    }
}
