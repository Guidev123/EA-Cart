using Cart.Application.UseCases;
using Cart.Application.UseCases.Cart.AddItem;
using Cart.Application.UseCases.Cart.ApplyVoucher;
using Cart.Application.UseCases.Item.Update;
using Cart.Application.UseCases.Voucher.Create;
using Cart.Application.UseCases.Voucher.Remove;
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
            builder.Services.AddTransient<IUseCase<Application.UseCases.Item.Remove.RemoveRequest, Application.UseCases.Item.Remove.RemoveResponse>, Application.UseCases.Item.Remove.RemoveHandler>();
            builder.Services.AddTransient<IUseCase<UpdateRequest, UpdateResponse>, UpdateHandler>();

            // CustomerCart
            builder.Services.AddTransient<IUseCase<ApplyVoucherRequest, ApplyVoucherResponse>, ApplyVoucherHandler>();
            builder.Services.AddTransient<IUseCase<AddItemRequest, AddItemResponse>, AddItemHandler>();

            // Voucher
            builder.Services.AddTransient<IUseCase<CreateRequest, CreateResponse>, CreateHandler>();
            builder.Services.AddTransient<IUseCase<Application.UseCases.Voucher.Remove.RemoveRequest, Application.UseCases.Voucher.Remove.RemoveResponse>, Application.UseCases.Voucher.Remove.RemoveHandler>();
        }

    }
}
