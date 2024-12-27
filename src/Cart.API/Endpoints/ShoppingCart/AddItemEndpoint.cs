using Cart.Application.Interfaces.Services;
using Cart.Application.Response;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Cart.AddItem;
using Cart.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Endpoints.ShoppingCart
{
    public class AddItemEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/", HandleAsync)
            .Produces<Response<AddItemToCartResponse>>()
            .WithDescription(@"Responsible for adding products to the cart");

        private static async Task<IResult> HandleAsync([FromServices] IUserService user,
                                                       [FromServices] ICartRepository cartRepository,
                                                       [FromServices] IUseCase<AddItemToCartRequest, AddItemToCartResponse> useCase,
                                                       AddItemToCartRequest request)
        {
            var userId = await user.GetUserIdAsync();
            if (userId is null) return TypedResults.BadRequest();

            request.AssociateCustomerId(userId.Value);

            var result = await useCase.HandleAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"/{userId}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
