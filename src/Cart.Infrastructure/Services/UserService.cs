using Cart.Application.Services;

namespace Cart.Infrastructure.Services
{
    public sealed class UserService : IUserService
    {
        public Task<Guid?> GetUserIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
