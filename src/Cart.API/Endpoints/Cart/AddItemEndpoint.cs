using Cart.Application.Services;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Cart.AddItem;
using Cart.Core.Repositories;

namespace Cart.API.Endpoints.Cart
{
    public class AddItemEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/", HandleAsync).Produces<IResult>();

        private static async Task<IResult> HandleAsync(IUserService user,
                                                       ICustomerCartRepository cartRepository,
                                                       IUseCase<AddItemRequest, AddItemResponse> useCase,
                                                       AddItemRequest request)
        {
            var userId = await user.GetUserIdAsync();
            if(userId is null) return TypedResults.BadRequest();

            var result = await useCase.HandleAsync(request);

            return result.IsSuccess 
                ? TypedResults.Created($"/{userId}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
