using Cart.Application;
using Cart.Infrastructure;
using Cart.Infrastructure.Persistence.Configurations;

namespace Cart.API.Configurations
{
    public static class ApplicationConfig
    {
        public static void AddApplicationConfig(this WebApplicationBuilder builder)
        {
            RegisterContextSettings(builder);
            builder.Services.AddApplication();
            builder.Services.AddInfra();
        }

        public static void RegisterContextSettings(this WebApplicationBuilder builder) =>
            builder.Services.Configure<ContextSettings>(builder.Configuration.GetSection(nameof(ContextSettings)));
    }
}
