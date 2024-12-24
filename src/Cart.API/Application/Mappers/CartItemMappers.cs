using Cart.API.Application.UseCases.CustomerCartCases.Handle;
using Cart.Core.Entities;

namespace Cart.API.Application.Mappers
{
    public static class CartItemMappers
    {
        public static CartItem MapToEntity(this HandleRequest request) =>
            new(request.ProductId, request.Name, request.Price, request.ImageUrl, request.Quantity);
    }
}
