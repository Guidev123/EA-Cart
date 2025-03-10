﻿using Cart.Application.DTOs;
using Cart.Application.UseCases.Cart.AddItem;
using Cart.Core.Entities;

namespace Cart.Application.Mappers
{
    public static class CartItemMappers
    {
        public static CartItem MapToEntity(this AddItemToCartRequest request) =>
            new(request.ProductId, request.Name, request.Price, request.ImageUrl, request.Quantity);

        public static CartItemDTO MapFromEntity(this CartItem entity) =>
            new(entity.ProductId, entity.Name, entity.Price, entity.ImageUrl, entity.Quantity);
    }
}
