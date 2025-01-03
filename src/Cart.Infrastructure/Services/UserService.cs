﻿using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Cart.Application.Services.AuthServices;

namespace Cart.Infrastructure.Services
{
    public sealed class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
    {
        private readonly ClaimsPrincipal _claims = httpContextAccessor.HttpContext!.User;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public HttpContext GetHttpContext() => _httpContextAccessor.HttpContext;

        public string GetToken()
        {
            var authorizationHeader = GetHttpContext().Request.Headers["Authorization"].ToString();

            if (authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                return authorizationHeader["Bearer ".Length..].Trim();

            return string.Empty;
        }


        public Task<Guid?> GetUserIdAsync()
        {
            var userIdClaim = _claims?.FindFirst("sub")?.Value ?? _claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdClaim, out var userId))
                return Task.FromResult<Guid?>(userId);

            return Task.FromResult<Guid?>(null);
        }

    }
}
