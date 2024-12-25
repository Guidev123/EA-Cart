using Cart.Core.Entities;
using Cart.Core.Repositories;

namespace Cart.Infrastructure.Persistence.Repositories
{
    public class CartRepository(CartDbContext context) : ICartRepository
    {
        private readonly CartDbContext _context = context;

        public Task AddCartItem(CartItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CartItemAlreadyExists(CartItem item)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(CustomerCart cart)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerCart> GetByCustomerIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem?> GetCartItemByIdAsync(Guid cartId, Guid productId)
        {
            throw new NotImplementedException();
        }

        public void RemoveCartItem(CartItem item)
        {
            throw new NotImplementedException();
        }

        public void UpdateCart(CustomerCart cart)
        {
            throw new NotImplementedException();
        }

        public void UpdateCartItem(CartItem item)
        {
            throw new NotImplementedException();
        }
    }
}
