﻿using Cart.Application.UseCases;
using Cart.Application.UseCases.CartItemCases.RemoveItem;
using Cart.Application.UseCases.CartItemCases.UpdateItem;
using Cart.Application.UseCases.CustomerCartCases.ApplyVoucher;
using Cart.Application.UseCases.CustomerCartCases.Handle;
using Cart.Application.UseCases.VoucherCases.Create;
using Cart.Application.UseCases.VoucherCases.Remove;
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
            builder.Services.AddTransient<IUseCase<HandleRequest, HandleResponse>, HandleHandler>();

            // Voucher
            builder.Services.AddTransient<IUseCase<CreateRequest, CreateResponse>, CreateHandler>();
            builder.Services.AddTransient<IUseCase<RemoveRequest, RemoveResponse>, RemoveHandler>();
        }

    }
}
