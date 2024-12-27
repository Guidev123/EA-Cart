﻿using Microsoft.AspNetCore.Http;

namespace Cart.Application.Services
{
    public interface IUserService
    {
        Task<Guid?> GetUserIdAsync();
        HttpContext GetHttpContext();
        string? GetToken();
    }
}
