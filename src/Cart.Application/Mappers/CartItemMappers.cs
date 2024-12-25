using Cart.Application.UseCases.Cart.AddItem;
using Cart.Core.Entities;

namespace Cart.Application.Mappers
{
    public static class CartItemMappers
    {
        public static CartItem MapToEntity(this AddItemToCartRequest request) =>
            new(request.ProductId, request.Name, request.Price, request.ImageUrl, request.Quantity);
    }
}
