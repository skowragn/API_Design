using Web_Api.DTOs.Authors;

namespace Web_Api.DTOs.Book;

public record BookDto
{
   public int BookId { get; set; }
   public string? Isbn { get; set; }
   public string? Title { get; set; }
   public string? Description { get; set; }
   public IEnumerable<AuthorDto> Authors { get; set; } = []; 
}
