
using Cart.Application.Response;
using Cart.Application.Services.AuthServices;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Cart.GetByCustomerId;

namespace Cart.API.Endpoints.ShoppingCart
{
    public class GetByCustomerIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync).Produces<Response<GetByCustomerIdResponse>>();

        private static async Task<IResult> HandleAsync(IUserService user,
                                                       IUseCase<GetByCustomerIdRequest, GetByCustomerIdResponse> useCase)
        {
            var userId = await user.GetUserIdAsync();
            if (userId is null) return TypedResults.BadRequest();

            var result = await useCase.HandleAsync(new(userId.Value));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.NotFound(result);
        }
    }
}
