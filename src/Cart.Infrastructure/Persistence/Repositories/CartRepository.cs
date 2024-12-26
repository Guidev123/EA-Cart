using Cart.Core.Entities;
using Cart.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure.Persistence.Repositories
{
    public class CartRepository(CartDbContext context) : ICartRepository
    {
        private readonly CartDbContext _context = context;
        public async Task<CustomerCart?> GetByCustomerIdAsync(Guid id) =>
            await _context.Carts.Include(x => x.Itens).FirstOrDefaultAsync(x => x.CustomerId == id);

        public async Task CreateAsync(CustomerCart cart) =>
            await _context.Carts.AddAsync(cart);

        public void UpdateCart(CustomerCart cart) =>
            _context.Carts.Update(cart);

        public async Task AddCartItem(CartItem item) =>
            await _context.CartItems.AddAsync(item);

        public async Task DeleteWhenOrderFinished(CustomerCart cart)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CartItemAlreadyExists(Guid itemId) =>
            await _context.CartItems.FromSqlInterpolated($"SELECT * FROM CartItens WHERE ProductId ={itemId}").AnyAsync();

        public async Task<CartItem?> GetCartItemByIdAsync(Guid cartId, Guid productId) =>
            await _context.CartItems.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == productId && x.CartId == cartId);

        public void RemoveCartItem(CartItem item) =>
            _context.CartItems.Remove(item);

        public void UpdateCartItem(CartItem item) =>
            _context.CartItems.Update(item);

    }
}
