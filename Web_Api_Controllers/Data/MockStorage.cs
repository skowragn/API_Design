using Web_Api_Controllers.DTOs.Authors;
using Web_Api_Controllers.DTOs.Book;
using Web_Api_Controllers.DTOs.Cart;

namespace Web_Api_Controllers.Data;

public class MockStorage
{
    private static List<CartDto> _carts = new List<CartDto>()
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

    private static List<AuthorDto> _authors = new List<AuthorDto>()
    {
        new AuthorDto() { AuthorId = 456, FullName = "VV" },
        new AuthorDto() { AuthorId = 789, FullName = "BB" }
    };

    private static List<BookDto> _books = new List<BookDto>()
    { 
        new BookDto()
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
        new BookDto()
        {
            BookId = 67891,
            Isbn = "VV",
            Title = "New",
            Description = "New book",
            Authors = new List<AuthorDto>()
                {
                    new() { AuthorId = 789, FullName = "BB" }
                }
        }
    };

    // Mock of Books storage actions
    public async static Task<IEnumerable<BookDto>> GetBookList()
    {
        return _books;
    }

    public async static Task<BookDto?> GetBookById(int id)
    {
        return _books.FirstOrDefault(b => b.BookId == id);
    }

    public async static Task<int> GetBookIndexById(int id)
    {
        return _books.FindIndex(b => b.BookId == id);
    }

    public async static Task RemoveBookByIndex(int index)
    {
        _books.RemoveAt(index);
    }

    public async static Task<BookDto> UpdateBookById(int id, BookDto updatedBook)
    {
        var index = _books.FindIndex(b => b.BookId == id);
        _books[index]  = updatedBook;
        return _books[index];

    }

    public async static Task AddBook(CreateBookDto book)
    {
        var newBook = new BookDto
        {
            BookId = book.BookId,
            Isbn = book.Isbn,
            Title = book.Title,
            Description = book.Description,
            Authors = book.Authors
        };
        _books.Add(newBook);
    }

    public async static Task<BookDto?> SearchBook(Query query)
    {
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query), "Query cannot be null");
        }

        if (!string.IsNullOrEmpty(query.Title) && _books.Count != 0)
        {
            return _books.FirstOrDefault(b => b.Title == query.Title);
        }

        if (!string.IsNullOrEmpty(query.Isbn))
        {
            return _books.FirstOrDefault(b => b.Isbn == query.Isbn);
        }

        if (!string.IsNullOrEmpty(query.AuthorFullName))
        {
            return _books.FirstOrDefault(b => b.Authors.Any(a => a.FullName == query.AuthorFullName));
        }

        return new BookDto();
    }

    // Mock of Carts storage actions
    public async static Task<CartDto?> GetCartById(int cartId)
    {
        return _carts.FirstOrDefault(c => c.CartId == cartId);
    }

    public async static Task<int> GetCartItemsIndex(CartDto cart, int cartItemId)
    {
        var itemIndex = cart?.Items?.ToList().FindIndex(i => i.CartItemId == cartItemId) ?? -1;
        return itemIndex;
    }

    public async static Task RemoveCartItemByIndex(int index, CartDto cart)
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

    public async static Task AddCart(CreateCartDto cart)
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

    public async static Task<int> GetCartIndexById(int id)
    {
        return _carts.FindIndex(b => b.CartId == id);
    }

    public async static Task RemoveCartByIndex(int index)
    {
        _carts.RemoveAt(index);
    }

    // Mock of Authors storage actions
    public async static Task<IEnumerable<AuthorDto>> GetAuthorList()
    {
        return _authors;
    }
}
