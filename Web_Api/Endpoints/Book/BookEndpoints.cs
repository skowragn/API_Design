using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;
using Web_Api.Data;
using Web_Api.DTOs.Book;
using Asp.Versioning;

namespace Web_Api.Endpoints.Book;

public class BookEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // GET books 200OK
        app.MapGet("/books", async (IBookService bookService) =>
           TypedResults.Ok(await bookService.GetBookList()))
          .WithName("GetBooks")
          .MapToApiVersion(new ApiVersion(1.0))
          .WithOpenApi(x => new OpenApiOperation(x)
          {
             Summary = "Get Books",
             Description = "Returns information about all the available books from the Bookstore.",
             Tags = new List<OpenApiTag> { new() { Name = "books" } }
          });

        // GET books/(bookId) 200OK, 404NotFound
        app.MapGet("/books/{bookId}", async Task<Results<Ok<BookDto>, NotFound>> (IBookService bookService, int bookId) =>
                await bookService.GetBookById(bookId) is { } book
                    ? TypedResults.Ok(book)
                    : TypedResults.NotFound()
         )
        .WithName("GetBookById")
        .MapToApiVersion(new ApiVersion(1.0))
        .WithOpenApi(x => new OpenApiOperation(x)
        {
           Summary = "Get book by Id",
           Description = "Returns information about selected book.",
           Tags = new List<OpenApiTag> { new() { Name = "books" } }
        });


        // POST books 201Created, 400BadRequest
        app.MapPost("/books", async (CreateBookDto book, IBookService bookService) =>
        {
           if (book == null) return Results.BadRequest();

           await bookService.AddBook(book);

           return Results.Created($"/books/{book.BookId}", book);
        })
         .WithName("Post")
         .MapToApiVersion(new ApiVersion(2.0))
         .WithOpenApi(x => new OpenApiOperation(x)
         {
            Summary = "Create book",
            Description = "Create new book and add it itnto Bookstore.",
            Tags = new List<OpenApiTag> { new() { Name = "books" } }
          });

        // PUT books/{bookId} 201Created, 404NotFound
        app.MapPut("/books/{bookId}", async (int bookId, UpdateBookDto book, IBookService bookService) =>
        {
           var existingBook = await bookService.GetBookById(bookId);

           if (existingBook == null) return Results.NotFound();

            existingBook ??= new BookDto()
            {
                Isbn = book.Isbn,
                Title = book.Title,
                Description = book.Description,
                Authors = book.Authors
            };

            var updatedBook = await bookService.UpdateBookById(bookId, existingBook);

            if (updatedBook != null)
                return Results.Created($"/books/{book.BookId}", book);
            return Results.NoContent(); 
        })
        .WithName("Put")
        .MapToApiVersion(new ApiVersion(2.0))
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Update the existing book item.",
            Description = "Update the existing book item.",
            Tags = new List<OpenApiTag> { new() { Name = "books" } }
        });

        // DELETE books/{bookId} 404NotFound, 204NoContent
        app.MapDelete("/books/{bookId}", async (int bookId, IBookService bookService) =>
        {
           var index = await bookService.GetBookIndexById(bookId);

           if (index < 0) return Results.NotFound();

           await bookService.RemoveBookByIndex(index);
           return Results.NoContent();
        })
         .WithName("Delete")
         .MapToApiVersion(new ApiVersion(2.0))
         .WithOpenApi(x => new OpenApiOperation(x)
         {
            Summary = "Delete the existing book item.",
            Description = "Delete the existing book item.",
            Tags = new List<OpenApiTag> { new() { Name = "books" } }
         });
    }
}
