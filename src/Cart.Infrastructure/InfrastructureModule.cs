using Cart.Application.Services;
using Cart.Core.Repositories;
using Cart.Infrastructure.Persistence;
using Cart.Infrastructure.Persistence.Repositories;
using Cart.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

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

        public static void AddRepositories(this IServiceCollection services) =>
            services.AddTransient<ICustomerCartRepository, CustomerCartRepository>();

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }
}
