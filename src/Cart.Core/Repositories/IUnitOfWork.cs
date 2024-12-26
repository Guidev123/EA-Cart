namespace Cart.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICartRepository Carts { get; }
        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task<bool> CommitAsync();
        bool HasActiveTransaction();
        Task RollbackTransactionAsync();
    }
}
