using Cart.Application.Response;
using Cart.Application.UseCases;
using Cart.Application.UseCases.Voucher.Remove;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Endpoints.Voucher
{
    public class RemoveVoucherEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapDelete("/{code}", HandleAsync).Produces<Response<RemoveVoucherResponse>>();

        private static async Task<IResult> HandleAsync([FromServices] IUseCase<RemoveVoucherRequest, RemoveVoucherResponse> useCase,
                                                       string code)
        {
            var result = await useCase.HandleAsync(new(code));
            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.BadRequest(result);
        }
    }
}
