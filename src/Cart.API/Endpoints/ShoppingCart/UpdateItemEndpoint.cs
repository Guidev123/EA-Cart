using Cart.Application.Response;
using Cart.Application.Services.AuthServices;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Item.Update;
using Cart.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Endpoints.ShoppingCart
{
    public class UpdateItemEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPut("/{productId:guid}/{quantity:int}", HandleAsync)
            .Produces<Response<UpdateCartItemResponse?>>()
            .WithDescription("Update units of an item within the cart");

        private static async Task<IResult> HandleAsync(Guid productId,
                                                       int quantity,
                                                       IUserService user,
                                                       IUseCase<UpdateCartItemRequest, UpdateCartItemResponse> useCase)
        {
            var userId = await user.GetUserIdAsync();
            if (userId is null) return TypedResults.BadRequest();

            var result = await useCase.HandleAsync(new(quantity, productId, userId.Value));

            return result.IsSuccess
                ? TypedResults.Ok()
                : TypedResults.BadRequest(result);

        }
    }
}
