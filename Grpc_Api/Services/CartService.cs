using Grpc.Core;

namespace Grpc_Api.Services;

public class CartService : Cart.CartBase
{
    private readonly ILogger<CartService> _logger;
    private static readonly List<CartReply> _allCartReply = [];

    public CartService(ILogger<CartService> logger)
    {
        _logger = logger;
        CartsStorageInit();
    }
    public override Task<CartReply> GetCartById(CartRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Get Cart with id:{ID}", request.CartId);
        var cartReply = _allCartReply.FirstOrDefault(c => c.CartId == request.CartId);
        if (cartReply == null)
        {
            _logger.LogWarning("Cart with id:{ID} not found", request.CartId);
            throw new RpcException(new Status(StatusCode.NotFound, $"Cart with id {request.CartId} not found"));
        }
        return Task.FromResult(cartReply);
    }

    public override Task<CreateCartReply> CreateCart(CreateCartRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Create Cart");

        if (request == null)
        {
            _logger.LogError("Request is null");
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Request cannot be null"));
        }
        if (request.Items == null || !request.Items.Any())
        {
            _logger.LogError("Items are required to create a cart");
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Items are required"));
        }
        if (string.IsNullOrEmpty(request.UserId))
        {
            _logger.LogError("UserId is required to create a cart");
            throw new RpcException(new Status(StatusCode.InvalidArgument, "UserId is required"));
        }
        var newCart = new CreateCartReply
        {
            CartId = _allCartReply.Count + 1,
            UserId = request.UserId,
        };

        newCart.Items.AddRange(request.Items);

        _logger.LogInformation("New Cart created with id:{ID}", newCart.CartId);

        var newCartReply = new CartReply
        {
            CartId = newCart.CartId,
            UserId = newCart.UserId
        };
        newCartReply.Items.AddRange(newCart.Items);

        _allCartReply.Add(newCartReply);

        return Task.FromResult(newCart);
    }

    public override Task<CartReply> DeleteCartById(CartRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Delete cart with id:{ID}", request.CartId);
        var cartToDelete = _allCartReply.FirstOrDefault(c => c.CartId == request.CartId);

        if (cartToDelete == null)
        {
            _logger.LogWarning("Cart with id:{ID} not found", request.CartId);
            throw new RpcException(new Status(StatusCode.NotFound, $"Cart with id {request.CartId} not found"));
        }

        _allCartReply.Remove(cartToDelete);
        return Task.FromResult(cartToDelete);
    }

    public override Task<CartReply> DeleteCartItemByIndex(DeleteCartItemRequest request, ServerCallContext context)
    {
        var cartIndex = _allCartReply.FindIndex(c => c.CartId == request.CartId);

        if (cartIndex < 0)
        {
            _logger.LogWarning("Cart with id:{ID} not found", request.CartId);
            throw new RpcException(new Status(StatusCode.NotFound, $"Cart with id {request.CartId} not found"));
        }

        var updatedCart = _allCartReply[cartIndex];

        var cartItemIndex = updatedCart.Items.ToList().FindIndex(itemIndex => itemIndex.CartItemId == request.CartItemId);

        if (cartItemIndex < 0) 
        {
            _logger.LogWarning("Cart Item with id:{ID} not found", request.CartItemId);
            throw new RpcException(new Status(StatusCode.NotFound, $"Cart Item with id {request.CartItemId} not found"));
        }

        updatedCart.Items.ToList().RemoveAt(cartItemIndex);
        _allCartReply[cartIndex] = updatedCart;

        return Task.FromResult(_allCartReply[cartIndex]);
    }

    private static void CartsStorageInit()
    {
       var authorItems = new List<CartAuthorTypeReply>
       {
           new CartAuthorTypeReply()
           {
               AuthorId = 1,
               FullName = "Author 1"
           }
       };
        var bookReply = new CartBookTypeReply()
        {
            BookId = 1,
            Isbn = "123",
            Title = "New book",
            Description = "New Modern book",
        };

        bookReply.Authors.Add(authorItems);


        var cartItems = new List<CartItemReply>
        {
            new CartItemReply
            {
                CartItemId = 1,
                Book = bookReply,
                Quantity = 2,
                Price = 20
            },
            new CartItemReply
            {
                CartItemId = 2,
                Book = bookReply,
                Quantity = 1,
                Price = 29
            }
        };


        var cart1 = new CartReply
        {
            CartId = 1,
            UserId = "User1"
        };
        cart1.Items.AddRange(cartItems);

        _allCartReply.Add(cart1);
    }
}
