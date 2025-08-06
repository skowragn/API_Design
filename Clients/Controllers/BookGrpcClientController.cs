using Asp.Versioning;
using Clients.Mappers;
using Grpc.Sdk;
using Grpc.Sdk.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Clients.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/grpc/books")]
    [ApiController]
    public class BookGrpcClientController : ControllerBase
    {
        private readonly IBookGrpcService _grpcBookService;
        public BookGrpcClientController(IBookGrpcService grpcBookService)
        {
            _grpcBookService = grpcBookService;
        }

        // GET: api/books  200OK
        [MapToApiVersion(1)]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookRestResponses>))]
        public async Task<ActionResult<IEnumerable<BookRestResponses>>> Get()
        {
            var bookResults = await _grpcBookService.GetBooks(new Grpc_Api.AllBookRequest(), CancellationToken.None);

            if (bookResults == null)
            {
                return NotFound("Book not found");
            }

            var books = bookResults.Books.Select(b => b.ToBookRestResponses()).ToList();

            return Ok(books);
        }

        // GET books/{bookId}   200OK, 404NotFound
       [MapToApiVersion(1)]
       [HttpGet("{bookId}")]
       [Consumes(MediaTypeNames.Application.Json)]
       [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookRestResponses))]
       [ProducesResponseType(StatusCodes.Status404NotFound)]
       public async Task<ActionResult<BookRestResponses>> Get(int bookId)
       {
          var bookResults = await _grpcBookService.GetBookById(new Grpc_Api.BookRequest() { BookId = bookId}, CancellationToken.None);
          
          if (bookResults == null)
          {
              return NotFound("Book not found");
          }

          var bookRestResposes = bookResults.ToBookRestResponses();
          return Ok(bookRestResposes);
       }


        // POST books  201Created, 400BadRequest
        [MapToApiVersion(1)]
        [MapToApiVersion(2)]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookRestResponses>> Post([FromBody] BookRestRequest book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            var createdBook = await _grpcBookService.CreateBook(new Grpc_Api.CreateBookRequest(), CancellationToken.None);

            var bookReply = createdBook.ToBookRestResponses();


            return CreatedAtAction(nameof(Get), bookReply, book);
        }

        // DELETE books/{bookId}  204NoContent, 404NotFound
        [MapToApiVersion(1)]
        [MapToApiVersion(2)]
        [HttpDelete("{bookId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(BookRestResponses))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BookRestResponses))]
        public async Task<IActionResult> Delete(int bookId)
        {
            var bookResults = await _grpcBookService.DeleteBookById(new Grpc_Api.BookRequest() { BookId = bookId }, CancellationToken.None);

            if (bookResults == null)
            {
                return NotFound("Book not found");
            }
            return NoContent();
        }
    }
}
