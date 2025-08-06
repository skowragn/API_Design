using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;
using Web_Api.Data;
using Web_Api.DTOs.Cart;

namespace Web_Api.Endpoints.Cart;

public class CartEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // GET carts/{cartId} 200OK, 404NotFound
        app.MapGet("/carts/{cartId}", async Task<Results<Ok<CartDto>, NotFound>> (ICartService cartService, int cartId) =>
                 await cartService.GetCartById(cartId) is { } cart
                     ? TypedResults.Ok(cart)
                     : TypedResults.NotFound()
          )
         .WithName("GetCartById")
         .MapToApiVersion(new ApiVersion(1.0))
         .WithOpenApi(x => new OpenApiOperation(x)
         {
             Summary = "Get cart by Id",
             Description = "Returns information about selected cart.",
             Tags = new List<OpenApiTag> { new() { Name = "carts" } }
         });

        //POST carts 201Created, 400BadRequest
        app.MapPost("/carts", async (CreateCartDto cart, ICartService cartService) =>
        {
            if (cart == null) return Results.BadRequest();

            await cartService.AddCart(cart);

            return Results.Created($"/carts/{cart.CartId}", cart);
        })
         .WithName("PostCart")
         .MapToApiVersion(new ApiVersion(1.0))
         .WithOpenApi(x => new OpenApiOperation(x)
         {
             Summary = "Create cart",
             Description = "Create new cart per user.",
             Tags = new List<OpenApiTag> { new() { Name = "carts" } }
         });

      
        // DELETE carts/{cartId} 204NoContent, 404NotFound
        app.MapDelete("/carts/{cartId}", async (int cartId, ICartService cartService) =>
        {
            var index = await cartService.GetCartIndexById(cartId);

            if (index < 0) return Results.NotFound();

            await cartService.RemoveCartByIndex(index);
            return Results.NoContent();
        })
         .WithName("DeleteCart")
         .MapToApiVersion(new ApiVersion(2.0))
         .WithOpenApi(x => new OpenApiOperation(x)
         {
             Summary = "Delete the existing cart.",
             Description = "Delete the existing cart.",
             Tags = new List<OpenApiTag> { new() { Name = "carts" } }
         });

        // DELETE /carts/{cartId}/items/{cartItemId} 204NoContent, 404NotFound
        app.MapDelete("/carts/{cartId}/items/{cartItemId}", async (int cartId, int cartItemId, ICartService cartService) =>
        {
            var cart = await cartService.GetCartById(cartId);

            if (cart == null)
            {
                return Results.NotFound();
            }

            var itemIndex = await cartService.GetCartItemsIndex(cart, cartItemId);

            if (cart == null || itemIndex < 0)
            {
                return Results.NotFound();
            }

            await cartService.RemoveCartItemByIndex(itemIndex, cart);
            return Results.NoContent();

        })
         .WithName("DeleteCartItem")
         .MapToApiVersion(new ApiVersion(2.0))
         .WithOpenApi(x => new OpenApiOperation(x)
         {
             Summary = "Delete the existing cart item from selected cart.",
             Description = "Delete the existing cart item from selected cart.",
             Tags = new List<OpenApiTag> { new() { Name = "carts" } }
         });
    }
}