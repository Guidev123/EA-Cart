using Cart.Application.Services.AuthServices;
using Cart.Application.Services.ExternalServices;
using Cart.Core.Repositories;
using Cart.Infrastructure.ExternalServices;
using Cart.Infrastructure.Persistence.Repositories;
using Cart.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cart.Infrastructure
{
    public static class InfrastructureModule
    {
        public static void AddInfra(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddServices();
            services.AddRestServices();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddRestServices(this  IServiceCollection services)
        {
            services.AddHttpClient<IVoucherRestService, VoucherRestService>();
        }
    }
}
