using Cart.Infrastructure.Persistence.Configurations;

namespace Cart.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ContextSettings>(builder.Configuration.GetSection(nameof(ContextSettings)));
        }
    }
}
