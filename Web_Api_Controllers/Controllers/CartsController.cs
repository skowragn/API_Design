using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using Web_Api_Controllers.Data;
using Web_Api_Controllers.DTOs.Book;
using Web_Api_Controllers.DTOs.Cart;

namespace Web_Api_Controllers.Controllers
{
    [ApiVersion(2)]
    [Route("api/v{version:apiVersion}/carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        // GET carts/{cartId} 200OK, 404NotFound
        [HttpGet("{cartId}")]
        [MapToApiVersion(1)]
        [MapToApiVersion(2)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CartDto))]
        public async Task<ActionResult<CartDto>> Get(int cartId)
        {
           var cart = await MockStorage.GetCartById(cartId);

            return cart;
        }

        // POST carts/{cartId}/items 201Created, 400NotFound
        [MapToApiVersion(2)]
        [HttpPost("{cartId}/items")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateCartDto>> Post(int cartId, [FromBody] IEnumerable<CartItemDto> cartItems)
        {
            if (cartItems == null || !cartItems.Any())
            {
                return BadRequest();
            }

            var cartDto = await MockStorage.AddItemsToCart(cartId,cartItems);

            return CreatedAtAction(nameof(Get), new CreateCartDto() { CartId = cartId }, cartDto);
        }

        // DELETE carts/{cartId}  204NoContent, 404NotFound
        [MapToApiVersion(2)]
        [HttpDelete("{cartId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CartDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CartDto))]
        public async Task<IActionResult> Delete(int cartId)
        {
            var index = await MockStorage.GetCartIndexById(cartId);

            if (index < 0)
            {
                return NotFound();
            }


            await MockStorage.RemoveCartByIndex(index);
            return NoContent();
        }

        // DELETE /carts/{cartId}/items/{cartItemId} 204NoContent, 404NotFound
        [MapToApiVersion(2)]
        [HttpDelete("{cartId}/items/{cartItemId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CartDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CartDto))]
        public async Task<IActionResult> Delete(int cartId, int cartItemId)
        {
            var cart = await MockStorage.GetCartById(cartId);

            if(cart == null)
            {
                return NotFound();
            }

            var itemIndex = await MockStorage.GetCartItemsIndex(cart, cartItemId);

            if (cart == null || itemIndex < 0)
            {
                return NotFound();
            }

            await MockStorage.RemoveCartItemByIndex(itemIndex, cart);
            return NoContent();
        }
    }
}