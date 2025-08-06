using Web_Api.DTOs.Authors;

namespace Web_Api.Data;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAuthorList();
}

public class AuthorService : IAuthorService
{
    private readonly List<AuthorDto> _authors;
    public AuthorService()
    {
        _authors = new List<AuthorDto>
        {
            new AuthorDto { AuthorId = 1, FullName = "Author One" },
            new AuthorDto { AuthorId = 2, FullName = "Author Two" },
            new AuthorDto { AuthorId = 3, FullName = "Author Three" }
        };
    }

   
    public async Task<IEnumerable<AuthorDto>> GetAuthorList()
    {
        return _authors;
    }
}