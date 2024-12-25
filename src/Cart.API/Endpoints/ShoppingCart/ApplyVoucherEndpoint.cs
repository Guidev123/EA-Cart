using Cart.Application.Response;
using Cart.Application.Services;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Cart.ApplyVoucher;
using Cart.Core.Entities;
using Cart.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Endpoints.ShoppingCart
{
    public class ApplyVoucherEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/apply-voucher/{voucherCode}", HandleAsync).Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync([FromServices] IUserService user,
                                                       [FromServices] ICartRepository cartRepository,
                                                       [FromServices] IUseCase<ApplyVoucherToCartRequest, ApplyVoucherToCartResponse> useCase,
                                                       string voucherCode)
        {
            var userId = await user.GetUserIdAsync();
            if (userId is null) return TypedResults.BadRequest();

            var result = await useCase.HandleAsync(new(voucherCode, userId.Value));

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }

    }
}
