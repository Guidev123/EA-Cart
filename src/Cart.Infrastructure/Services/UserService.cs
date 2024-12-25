using Cart.Application.Services;
using System.Security.Claims;

namespace Cart.Infrastructure.Services
{
    public sealed class UserService(ClaimsPrincipal claims) : IUserService
    {
        private readonly ClaimsPrincipal _claims = claims;
        public Task<Guid?> GetUserIdAsync()
        {
            var userIdClaim = _claims.FindFirst("sub")?.Value ?? _claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdClaim, out var userId))
                return Task.FromResult<Guid?>(userId);

            return Task.FromResult<Guid?>(null);
        }
    }
}
