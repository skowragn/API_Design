using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Web_Api_Controllers.Data;
using Web_Api_Controllers.DTOs.Book;

namespace Web_Api_Controllers.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{version:apiVersion}/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: api/books  200OK
        [MapToApiVersion(1)]
        [MapToApiVersion(2)]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        public async Task<ActionResult<BookDto>> Get()
        {
            var bookList = await MockStorage.GetBookList();

            return Ok(bookList);
        }

        // GET books/{bookId}   200OK, 404NotFound
        [MapToApiVersion(1)]
        [MapToApiVersion(2)]
        [HttpGet("{bookId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDto>> Get(int bookId)
        {
           var book = await MockStorage.GetBookById(bookId);
            
            if (bookId <= 0)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST books  201Created, 400BadRequest
        [MapToApiVersion(1)]
        [MapToApiVersion(2)]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateBookDto>> Post([FromBody] CreateBookDto book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            await MockStorage.AddBook(book);

           
            return CreatedAtAction(nameof(Get), new CreateBookDto() { BookId = book.BookId}, book);
        }

        // POST books/search 200OK
        [MapToApiVersion(1)]
        [MapToApiVersion(2)]
        [HttpPost("search")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        public async Task<ActionResult<IEnumerable<BookDto>>> Post(Query query)
        {
           
            var results = await MockStorage.SearchBook(query);

            return Ok(results);
        }

        // PUT books/{bookId}
        [MapToApiVersion(1)]
        [MapToApiVersion(2)]
        [HttpPut("{bookId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int bookId, [FromBody] UpdateBookDto book)
        {
            var existingBook = await MockStorage.GetBookById(bookId);

            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook ??= new BookDto()
            {
                Isbn = book.Isbn,
                Title = book.Title,
                Description = book.Description,
                Authors = book.Authors
            };

            var updatedBook = await MockStorage.UpdateBookById(bookId, existingBook);
            if(updatedBook != null)
            return CreatedAtAction(nameof(Get), new CreateBookDto() { BookId = book.BookId }, book);
            return NoContent();
        }

        // DELETE books/{bookId}  204NoContent, 404NotFound
        [MapToApiVersion(1)]
        [MapToApiVersion(2)]
        [HttpDelete("{bookId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(BookDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BookDto))]
        public async Task<IActionResult> Delete(int bookId)
        {
            var index = await MockStorage.GetBookIndexById(bookId);

            if(index < 0)
            {
               return NotFound();
            }


            await MockStorage.RemoveBookByIndex(index);
            return NoContent();

        }
    }
}
