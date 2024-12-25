using Cart.Application.Response;
using Cart.Application.Services;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Item.Remove;
using Cart.Core.Entities;
using Cart.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cart.API.Endpoints.ShoppingCart
{
    public class RemoveItemEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapDelete("/{productId:guid}", HandleAsync).Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync(Guid productId,
                                                       [FromServices] IUserService user,
                                                       [FromServices] ICartRepository cartRepository,
                                                       [FromServices] IUseCase<RemoveItemFromCartRequest, RemoveItemFromCartResponse> useCase)
        {
            var userId = await user.GetUserIdAsync();
            if (userId is null) return TypedResults.BadRequest();

            var result = await useCase.HandleAsync(new(productId, userId.Value));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
