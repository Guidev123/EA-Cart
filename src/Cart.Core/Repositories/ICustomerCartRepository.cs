using Cart.Core.Entities;

namespace Cart.Core.Repositories
{
    public interface ICustomerCartRepository
    {
        Task CreateAsync(CustomerCart cart);
        void UpdateCart(CustomerCart cart);
        Task<CustomerCart> GetByCustomerIdAsync(Guid id);

        Task<bool> CartItemAlreadyExists(CartItem item);
        void UpdateCartItem(CartItem item);
        Task AddCartItem(CartItem item);
    }
}
