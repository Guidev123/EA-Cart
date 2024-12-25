using Cart.Application.Services;
using Cart.Core.Repositories;
using Cart.Infrastructure.Persistence;
using Cart.Infrastructure.Persistence.Repositories;
using Cart.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cart.Infrastructure
{
    public static class InfrastructureModule
    {
        public static void AddInfra(this IServiceCollection services)
        {
            services.AddContextDependencyInjection();
            services.AddRepositories();
            services.AddServices();
        }

        public static void AddContextDependencyInjection(this IServiceCollection services) =>
            services.AddDbContext<CartDbContext>();

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
