using Cart.API.Endpoints.ShoppingCart;
using Cart.API.Endpoints.Voucher;

namespace Cart.API.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("api/v1/carts")
                .WithTags("Carts")
                .RequireAuthorization()
                .MapEndpoint<AddItemEndpoint>()
                .MapEndpoint<ApplyVoucherEndpoint>()
                .MapEndpoint<RemoveItemEndpoint>()
                .MapEndpoint<UpdateItemEndpoint>();

            endpoints.MapGroup("api/v1/vouchers")
                .WithTags("Vouchers")
                .RequireAuthorization()
                .MapEndpoint<CreateVoucherEndpoint>()
                .MapEndpoint<RemoveVoucherEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
