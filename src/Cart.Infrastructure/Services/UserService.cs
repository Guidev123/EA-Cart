using Cart.Application.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Cart.Infrastructure.Services
{
    public sealed class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
    {
        private readonly ClaimsPrincipal _claims = httpContextAccessor.HttpContext!.User;
        public Task<Guid?> GetUserIdAsync()
        {
            var userIdClaim = _claims?.FindFirst("sub")?.Value ?? _claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdClaim, out var userId))
                return Task.FromResult<Guid?>(userId);

            return Task.FromResult<Guid?>(null);
        }
    }
}
