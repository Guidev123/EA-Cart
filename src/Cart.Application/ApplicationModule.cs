using Cart.Application.BackgroundServices;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Cart.AddItem;
using Cart.Application.UseCases.Cart.ApplyVoucher;
using Cart.Application.UseCases.Cart.GetByCustomerId;
using Cart.Application.UseCases.Item.Remove;
using Cart.Application.UseCases.Item.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLib.MessageBus;

namespace Cart.Application
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddUseCases(services);
            RegisterBackgroundService(services, configuration);
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IUseCase<RemoveItemFromCartRequest, RemoveItemFromCartResponse>, RemoveItemFromCartHandler>();
            services.AddTransient<IUseCase<UpdateCartItemRequest, UpdateCartItemResponse>, UpdateCartItemHandler>();

            services.AddTransient<IUseCase<ApplyVoucherToCartRequest, ApplyVoucherToCartResponse>, ApplyVoucherToCartHandler>();
            services.AddTransient<IUseCase<AddItemToCartRequest, AddItemToCartResponse>, AddItemToCartHandler>();
            services.AddTransient<IUseCase<GetByCustomerIdRequest, GetByCustomerIdResponse>, GetByCustomerIdHandler>();
        }

        public static void RegisterBackgroundService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
            services.AddHostedService<CartBackgroundService>();
        }
    }
}
