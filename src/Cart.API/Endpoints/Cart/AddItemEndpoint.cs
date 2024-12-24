using Cart.API.Application.Services;
using Cart.API.Application.UseCases;
using Cart.API.Application.UseCases.CustomerCartCases.Handle;
using Cart.Core.Repositories;

namespace Cart.API.Endpoints.Cart
{
    public class AddItemEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/", HandleAsync).Produces<IResult>();

        private static async Task<IResult> HandleAsync(IUserService user,
                                                       ICustomerCartRepository cartRepository,
                                                       IUseCase<HandleRequest, HandleResponse> useCase,
                                                       HandleRequest request)
        {
            var userId = await user.GetUserIdAsync();
            if(userId.Equals(Guid.Empty)) return TypedResults.BadRequest();

            var result = await useCase.HandleAsync(request);

            return result.IsSuccess 
                ? TypedResults.Created(string.Empty, result)
                : TypedResults.BadRequest(result);
        }
    }
}
