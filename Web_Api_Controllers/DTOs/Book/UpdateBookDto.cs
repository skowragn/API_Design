using Web_Api_Controllers.DTOs.Authors;

namespace Web_Api_Controllers.DTOs.Book;

public class UpdateBookDto
{
    public int BookId { get; set; }
    public string? Isbn { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IEnumerable<AuthorDto> Authors { get; set; } = [];
}
