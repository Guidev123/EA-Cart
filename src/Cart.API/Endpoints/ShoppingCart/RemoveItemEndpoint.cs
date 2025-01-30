using Cart.Application.Response;
using Cart.Application.Services.AuthServices;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Item.Remove;
using Cart.Core.Repositories;

namespace Cart.API.Endpoints.ShoppingCart
{
    public class RemoveItemEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapDelete("/{productId:guid}", HandleAsync)
            .Produces<Response<RemoveItemFromCartResponse?>>()
            .WithDescription("Remove the item and its units from the cart");

        private static async Task<IResult> HandleAsync(Guid productId,
                                                       IUserService user,
                                                       ICartRepository cartRepository,
                                                       IUseCase<RemoveItemFromCartRequest, RemoveItemFromCartResponse> useCase)
        {
            var userId = await user.GetUserIdAsync();
            if (userId is null) return TypedResults.BadRequest();

            var result = await useCase.HandleAsync(new(productId, userId.Value));
            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.BadRequest(result);
        }
    }
}
