using Cart.Application.Interfaces.Services;
using Cart.Application.Response;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Cart.ApplyVoucher;
using Cart.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Endpoints.ShoppingCart
{
    public class ApplyVoucherEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/apply-voucher/{code}", HandleAsync)
            .Produces<Response<ApplyVoucherToCartResponse?>>()
            .WithDescription("Adds a discount voucher to the cart based on the voucher code");

        private static async Task<IResult> HandleAsync([FromServices] IUserService user,
                                                       [FromServices] ICartRepository cartRepository,
                                                       [FromServices] IUseCase<ApplyVoucherToCartRequest, ApplyVoucherToCartResponse> useCase,
                                                       string code)
        {
            var userId = await user.GetUserIdAsync();
            if (userId is null) return TypedResults.BadRequest();

            var result = await useCase.HandleAsync(new(code, userId.Value));

            return result.IsSuccess
                ? TypedResults.Ok()
                : TypedResults.BadRequest(result);
        }

    }
}
