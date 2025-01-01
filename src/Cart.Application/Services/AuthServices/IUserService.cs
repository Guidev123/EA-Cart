using Microsoft.AspNetCore.Http;

namespace Cart.Application.Services.AuthServices
{
    public interface IUserService
    {
        Task<Guid?> GetUserIdAsync();
        string GetToken();
    }
}
