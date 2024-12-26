using Cart.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cart.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork(ICartRepository cartRepository,
                            CartDbContext context) : IUnitOfWork
    {
        private IDbContextTransaction? _transaction;
        private readonly CartDbContext _context = context;
        private readonly ICartRepository _cartRepository = cartRepository;

        public ICartRepository Carts => _cartRepository;

        public async Task BeginTransactionAsync() =>
            _transaction = await _context.Database.BeginTransactionAsync();

        public bool HasActiveTransaction() => _transaction is not null;

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
                _transaction = null;
            }
        }


        public async Task<bool> CommitAsync()
        {
            if(_transaction is null) return false;

            try
            {
                await _transaction!.CommitAsync();
                return true;
            }
            catch
            {
                await _transaction!.RollbackAsync();
                return false;
            }
        }

        public async Task<int> CompleteAsync()
        {
            const int maxRetryCount = 3;
            for (int attempt = 1; attempt <= maxRetryCount; attempt++)
            {
                try
                {
                    return await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (attempt == maxRetryCount) throw;

                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is not null)
                        {
                            await entry.ReloadAsync();
                        }
                    }
                }
            }
            return 0;
        }

        public void Dispose()
        {
            _transaction?.Dispose();    
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
