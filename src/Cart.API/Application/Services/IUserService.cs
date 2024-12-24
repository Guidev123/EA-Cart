namespace Cart.API.Application.Services
{
    public interface IUserService
    {
        Task<Guid> GetUserIdAsync();
    }
}
