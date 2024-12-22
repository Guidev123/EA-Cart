using Cart.API.Application.UseCases;
using Cart.API.Application.UseCases.CartItem.RemoveItem;
using Cart.API.Application.UseCases.CartItem.UpdateItem;
using Cart.API.Application.UseCases.CustomerCart.ApplyVoucher;
using Cart.API.Application.UseCases.CustomerCart.HandleExistent;
using Cart.API.Application.UseCases.CustomerCart.HandleNew;
using Cart.API.Application.UseCases.Voucher.Create;
using Cart.API.Application.UseCases.Voucher.Remove;
using Cart.Infrastructure.Persistence.Configurations;

namespace Cart.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterDependencies(this WebApplicationBuilder builder)
        {

            builder.Services.Configure<ContextSettings>(builder.Configuration.GetSection(nameof(ContextSettings)));
        }

        public static void AddUseCases(this WebApplicationBuilder builder)
        {
            // CartItem
            builder.Services.AddTransient<IUseCase<RemoveItemRequest, RemoveItemResponse>, RemoveItemHandler>();
            builder.Services.AddTransient<IUseCase<UpdateItemRequest, UpdateItemResponse>, UpdateItemHandler>();

            // CustomerCart
            builder.Services.AddTransient<IUseCase<ApplyVoucherRequest, ApplyVoucherResponse>, ApplyVoucherHandler>();
            builder.Services.AddTransient<IUseCase<HandleExistentRequest, HandleExistentResponse>, HandleExistentHandler>();
            builder.Services.AddTransient<IUseCase<HandleNewRequest, HandleNewResponse>, HandleNewHandler>();

            // Voucher
            builder.Services.AddTransient<IUseCase<CreateRequest, CreateResponse>, CreateHandler>();
            builder.Services.AddTransient<IUseCase<RemoveRequest, RemoveResponse>, RemoveHandler>();
        }
    }
}
