using Cart.Application.Response;
using Cart.Application.Services.AuthServices;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Cart.ApplyVoucher;
using Cart.Core.Repositories;

namespace Cart.API.Endpoints.ShoppingCart
{
    public class ApplyVoucherEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/apply-voucher/{code}", HandleAsync)
            .Produces<Response<ApplyVoucherToCartResponse?>>()
            .WithDescription("Adds a discount voucher to the cart based on the voucher code");

        private static async Task<IResult> HandleAsync(IUserService user,
                                                       ICartRepository cartRepository,
                                                       IUseCase<ApplyVoucherToCartRequest, ApplyVoucherToCartResponse> useCase,
                                                       string code)
        {
            var result = await useCase.HandleAsync(new(code));

            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.BadRequest(result);
        }

    }
}
