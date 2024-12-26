using Cart.Application.UseCases.Cart.AddItem;
using Cart.Application.UseCases.Cart.ApplyVoucher;
using Cart.Application.UseCases.Item.Remove;
using Cart.Application.UseCases.Item.Update;
using Cart.Application.UseCases.Voucher.Create;
using Cart.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Cart.Application
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services) =>
            AddUseCases(services);

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
    }
}
