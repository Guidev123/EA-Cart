using Cart.Application.UseCases.Cart.AddItem;
using Cart.Application.UseCases.Cart.ApplyVoucher;
using Cart.Application.UseCases.Item.Remove;
using Cart.Application.UseCases.Item.Update;
using Cart.Application.UseCases.Voucher.Create;
using Cart.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Cart.Application.BackgroundServices;
using SharedLib.MessageBus;
using Microsoft.Extensions.Configuration;

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
            // CartItem
            services.AddTransient<IUseCase<RemoveItemFromCartRequest, RemoveItemFromCartResponse>, RemoveItemFromCartHandler>();
            services.AddTransient<IUseCase<UpdateCartItemRequest, UpdateCartItemResponse>, UpdateCartItemHandler>();

            // CustomerCart
            services.AddTransient<IUseCase<ApplyVoucherToCartRequest, ApplyVoucherToCartResponse>, ApplyVoucherToCartHandler>();
            services.AddTransient<IUseCase<AddItemToCartRequest, AddItemToCartResponse>, AddItemToCartHandler>();

            // Voucher
            services.AddTransient<IUseCase<CreateVoucherRequest, CreateVoucherResponse>, CreateVoucherHandler>();
        }

        public static void RegisterBackgroundService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
            services.AddHostedService<CartBackgroundService>();
        }
    }
}
