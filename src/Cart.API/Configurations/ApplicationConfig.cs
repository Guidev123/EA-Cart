using Cart.Infrastructure;

namespace Cart.API.Configurations
{
    public static class ApplicationConfig
    {
        public static void AddApplicationConfig(this WebApplicationBuilder builder)
        {
            builder.RegisterDependencies();
            builder.Services.AddInfra();
        }
    }
}
