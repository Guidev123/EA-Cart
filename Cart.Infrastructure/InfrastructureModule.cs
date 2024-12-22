using Cart.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Cart.Infrastructure
{
    public static class InfrastructureModule
    {
        public static void AddInfra(this IServiceCollection services)
        {
            services.AddContextDependencyInjection();
        }

        public static void AddContextDependencyInjection(this IServiceCollection services) =>
            services.AddDbContext<CartDbContext>();
    }
}
