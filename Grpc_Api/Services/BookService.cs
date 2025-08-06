using Grpc.Core;

namespace Grpc_Api.Services;

public class BookService : Book.BookBase
{
    private readonly ILogger<BookService> _logger;
    private static readonly List<AuthorTypeReply> _authorItems = [];
    private readonly BookReply _bookReply;
    private static readonly List<BookReply> _allBookReply = [];
    private static readonly AllBookReply _allBooks = new();

    public BookService(ILogger<BookService> logger)
    {
        _logger = logger;
        BooksStorageInit();
    }

    public override Task<BookReply> GetBookById(BookRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Get Book with id:{ID}", request.BookId);
        return Task.FromResult(_bookReply);
    }

    public override Task<AllBookReply> GetBooks(AllBookRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Get All Books");
        return Task.FromResult(_allBooks);
    }

    public override Task<BookReply> DeleteBookById(BookRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Delete book with id:{ID}", request.BookId);

         var bookToDelete = _allBooks.Books.FirstOrDefault(b => b.BookId == request.BookId);

        return Task.FromResult(bookToDelete);

    }

    public override Task<CreateBookReply> CreateBook(CreateBookRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Create new book");

        var newBook = new CreateBookReply
        {
            BookId = _allBooks.Books.Count + 1,
            Isbn = request.Isbn,
            Title = request.Title,
            Description = request.Description
        };

        _authorItems.Add(new AuthorTypeReply()
        {
            AuthorId = 1,
            FullName = "Author 1"
        });


        newBook.Authors.Add(_authorItems);

        var newBookReply = new BookReply
        {
            BookId = newBook.BookId,
            Isbn = newBook.Isbn,
            Title = newBook.Title,
            Description = newBook.Description
        };
        newBookReply.Authors.Add(_authorItems);

        _allBookReply.Add(newBookReply);

        _allBooks.Books.Add(newBookReply);

        return Task.FromResult(newBook);
    }

    private static void BooksStorageInit()
    {
        _authorItems.Add(new AuthorTypeReply()
        {
            AuthorId = 1,
            FullName = "Author 1"
        });

        var _bookReply = new BookReply
        {
            BookId = 1,
            Isbn = "123",
            Title = "New book",
            Description = "New Modern book",
        };

        var _newBookReply = new BookReply
        {
            BookId = 2,
            Isbn = "432",
            Title = "Old book",
            Description = "Old book",
        };

        _bookReply.Authors.Add(_authorItems);
        _newBookReply.Authors.Add(_authorItems);

        _allBookReply.Add(_bookReply);

        _allBooks.Books.Add(_bookReply);
        _allBooks.Books.Add(_newBookReply);
    }
}