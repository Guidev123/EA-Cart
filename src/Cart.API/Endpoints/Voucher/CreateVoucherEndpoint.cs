using Cart.Application.Response;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Cart.AddItem;
using Cart.Application.UseCases.Voucher.Create;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Endpoints.Voucher
{
    public class CreateVoucherEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/", HandleAsync).Produces<Response<CreateVoucherResponse>>();

        private static async Task<IResult> HandleAsync([FromServices] IUseCase<CreateVoucherRequest, CreateVoucherResponse> useCase,
                                                       CreateVoucherRequest request)
        {
            var result = await useCase.HandleAsync(request);
            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.BadRequest(result);
        }
    }
}
