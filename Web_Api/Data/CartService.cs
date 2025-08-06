using Web_Api.DTOs.Authors;
using Web_Api.DTOs.Book;
using Web_Api.DTOs.Cart;

namespace Web_Api.Data;

public interface ICartService
{
    Task<IEnumerable<CartDto>> GetCartList();
    Task<CartDto?> GetCartById(int cartId);
    Task<int> GetCartItemsIndex(CartDto cart, int cartItemId);
    Task RemoveCartItemByIndex(int index, CartDto cart);
    Task AddCart(CreateCartDto cart);
    Task<int> GetCartIndexById(int id);
    Task RemoveCartByIndex(int index);
}
public class CartService : ICartService
{
    private readonly List<CartDto> _carts;
    public CartService()
    {
        _carts = new List<CartDto>()
        {
           new CartDto()
        {
            CartId = 333,
            UserId = "user123",
            Items = new List<CartItemDto>()
            {
                new CartItemDto()
                {
                    CartItemId = 888,
                    Book = new BookDto()
                    {
                        BookId = 12345,
                        Isbn = "X1223",
                        Title = "DDD",
                        Description = "DDD",
                        Authors = new List<AuthorDto>()
                            {
                                new() { AuthorId = 456, FullName = "VV" }
                            }
                    },
                    Quantity = 1,
                    Price = 20
                },
                new CartItemDto()
                {
                    CartItemId = 889,
                    Book = new BookDto()
                    {
                        BookId = 6789,
                        Isbn = "X1223",
                        Title = "nnn",
                        Description = "New",
                        Authors = new List<AuthorDto>()
                            {
                                new() { AuthorId = 88, FullName = "WW" }
                            }
                    },
                    Quantity = 2,
                    Price = 100
                }
            }

        },
           new CartDto()
        {
            CartId = 555,
            UserId = "user5",
            Items = new List<CartItemDto>()
            {
                new CartItemDto()
                {
                    CartItemId = 777,
                    Book = new BookDto()
                    {
                        BookId = 12,
                        Isbn = "X",
                        Title = "AAA",
                        Description = "BBB",
                        Authors = new List<AuthorDto>()
                            {
                                new() { AuthorId = 999, FullName = "h" }
                            }
                    },
                    Quantity = 1,
                    Price = 20
                }
            }
        }
        };
    }
    public async Task<IEnumerable<CartDto>> GetCartList()
    {
        return _carts;
    }

    public async Task<CartDto?> GetCartById(int cartId)
    {
        return _carts.FirstOrDefault(c => c.CartId == cartId);
    }

    public async Task<int> GetCartItemsIndex(CartDto cart, int cartItemId)
    {
        var itemIndex = cart?.Items?.ToList().FindIndex(i => i.CartItemId == cartItemId) ?? -1;
        return itemIndex;
    }

    public async Task RemoveCartItemByIndex(int index, CartDto cart)
    {
        var cartItems = cart.Items.ToList();

        cartItems.RemoveAt(index);

        var cartIndex = _carts.FindIndex(b => b.CartId == cart.CartId); 

        if (cartIndex >= 0)
        {
            _carts[cartIndex].Items = cartItems;
        }
        else
        {
            throw new ArgumentException("Cart not found", nameof(cart.CartId));
        }

    }

    public async Task AddCart(CreateCartDto cart)
    {
        var newCart = new CartDto
        {
            CartId = cart.CartId,
            UserId = cart.UserId,
            Items = cart.Items.Select(item => new CartItemDto
            {
                CartItemId = item.CartItemId,
                Book = item.Book,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList()
        };
        _carts.Add(newCart);
    }

    public async Task<int> GetCartIndexById(int id)
    {
        return _carts.FindIndex(b => b.CartId == id);
    }

    public async Task RemoveCartByIndex(int index)
    {
        _carts.RemoveAt(index);
    }
}
