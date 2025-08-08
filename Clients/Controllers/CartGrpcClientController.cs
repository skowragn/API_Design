using Asp.Versioning;
using Clients.Mappers;
using Grpc.Sdk.DTOs;
using Grpc.Sdk.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Clients.Controllers;

[ApiVersion(1)]
[ApiVersion(2)]
[Route("api/grpcClient/v{version:apiVersion}/carts")]
[ApiController]
public class CartGrpcClientController : ControllerBase
{
    private readonly ICartGrpcService _grpcCartService;
    public CartGrpcClientController(ICartGrpcService grpcCartService)
    {
        _grpcCartService = grpcCartService;
    }

    // GET carts/{cartId} 200OK, 404NotFound
    [HttpGet("{cartId}")]
    [MapToApiVersion(2)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartRestResponses))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CartRestResponses))]
    public async Task<ActionResult<CartRestResponses>> Get(int cartId)
    {
        var cartResults = await _grpcCartService.GetCartById(new Grpc_Api.CartRequest() { CartId = cartId }, CancellationToken.None);

        if (cartResults == null)
        {
            return NotFound("Cart not found");
        }

        var cartRestResposes = cartResults.ToCartRestResponses();
        return Ok(cartRestResposes);

    }

    // POST carts/{cartId}/items 201Created, 400NotFound
    [MapToApiVersion(2)]
    [HttpPost("{cartId}/items")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created,Type = typeof(CreateCartRestResponses))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateCartRestResponses))]
    public async Task<ActionResult<CreateCartRestResponses>> Post(int cartId, [FromBody] Grpc_Api.CreateCartRequest cartRequest)
    {
        if (cartRequest == null)
        {
            return BadRequest();
        }
        var createCartReply = await _grpcCartService.CreateCart(cartRequest, CancellationToken.None);

        return CreatedAtAction(nameof(Get), new CreateCartRestResponses() { CartId = cartId }, createCartReply);
    }

    // DELETE carts/{cartId}  204NoContent, 404NotFound
    [MapToApiVersion(1)]
    [HttpDelete("{cartId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int cartId)
    {
       var cartToDelete = await _grpcCartService.DeleteCartById(new Grpc_Api.CartRequest() { CartId = cartId }, CancellationToken.None);

       if (cartToDelete == null)
       {
          return NotFound("Cart not found");
       }
       return NoContent();
    }

    // DELETE /carts/{cartId}/items/{cartItemId} 204NoContent, 404NotFound
    [MapToApiVersion(1)]
    [HttpDelete("{cartId}/items/{cartItemId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int cartId, int cartItemId)
    {
        var cart = await _grpcCartService.DeleteCartItemById(new Grpc_Api.DeleteCartItemRequest() { CartId = cartId, CartItemId = cartItemId }, CancellationToken.None);

        if (cart == null)
        {
            return NotFound("Cart not found");
        }
        return NoContent();
    }
 }
