using Cart.Application.Response;
using Cart.Application.Services;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Item.Update;
using Cart.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Endpoints.ShoppingCart
{
    public class UpdateItemEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPut("/{productId:guid}", HandleAsync).Produces<Response<CustomerCart?>>();

        private static async Task<IResult> HandleAsync(Guid productId,
                                                       [FromServices] UpdateCartItemRequest request,
                                                       [FromServices] IUserService user,
                                                       [FromServices] IUseCase<UpdateCartItemRequest, UpdateCartItemResponse> useCase)
        {
            var userId = await user.GetUserIdAsync();
            if (userId is null) return TypedResults.BadRequest();

            request.ProductId = productId;

            var result = await useCase.HandleAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);

        }
    }
}
