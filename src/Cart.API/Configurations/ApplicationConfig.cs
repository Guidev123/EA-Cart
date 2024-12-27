using Cart.Application;
using Cart.Infrastructure;
using Cart.Infrastructure.ExternalServices.Configurations;
using Cart.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cart.API.Configurations
{
    public static class ApplicationConfig
    {
        public static void AddApplicationConfig(this WebApplicationBuilder builder)
        {
            AddContextDependencyInjection(builder);
            RegisterExtensions(builder);
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddInfra();
            builder.Services.AddHttpContextAccessor();
        }

        public static void AddContextDependencyInjection(this WebApplicationBuilder builder) =>
        builder.Services.AddDbContext<CartDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetSection("ContextSettings")["ConnectionString"])
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors();
        });

        public static void RegisterExtensions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<VoucherRestServiceConfig>(builder.Configuration.GetSection("VoucherRestServiceConfig"));
        }
    }
}
