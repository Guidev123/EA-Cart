namespace Cart.Application.Services
{
    public interface IUserService
    {
        Task<Guid?> GetUserIdAsync();
        string GetUsetToken();
    }
}
