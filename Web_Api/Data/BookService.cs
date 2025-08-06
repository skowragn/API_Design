using Web_Api.DTOs.Book;
using Web_Api.DTOs.Authors;

namespace Web_Api.Data;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetBookList();
    Task<BookDto?> GetBookById(int id);
    Task<int> GetBookIndexById(int id);
    Task RemoveBookByIndex(int index);
    Task<BookDto> UpdateBookById(int id, BookDto updatedBook);
    Task AddBook(CreateBookDto book);
    Task<BookDto?> SearchBook(Query query);
}
public class BookService : IBookService
{
    private readonly List<BookDto> _books;
    public BookService() 
    { 

       _books = new List<BookDto>()
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
    }

    // Mock of Books storage actions
    public async Task<IEnumerable<BookDto>> GetBookList()
    {
        return _books;
    }

    public async Task<BookDto?> GetBookById(int id)
    {
        return _books.FirstOrDefault(b => b.BookId == id);
    }

    public async Task<int> GetBookIndexById(int id)
    {
        return _books.FindIndex(b => b.BookId == id);
    }

    public async Task RemoveBookByIndex(int index)
    {
        _books.RemoveAt(index);
    }

    public async Task<BookDto> UpdateBookById(int id, BookDto updatedBook)
    {
        var index = _books.FindIndex(b => b.BookId == id);
        _books[index] = updatedBook;
        return _books[index];
    }

    public async Task AddBook(CreateBookDto book)
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

    public async Task<BookDto?> SearchBook(Query query)
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

}
