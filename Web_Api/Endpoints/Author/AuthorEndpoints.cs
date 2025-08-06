using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Web_Api.Data;

namespace Web_Api.Endpoints.Author;

public class AuthorEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // GET authors 200OK
        app.MapGet("/authors", async (IAuthorService authorService) =>
                  TypedResults.Ok(await authorService.GetAuthorList()))
           .WithName("GetAuthors")
           .MapToApiVersion(new ApiVersion(2.0))
           .WithOpenApi(x => new OpenApiOperation(x)
           {
               Summary = "Get All Authors",
               Description = "Returns information about all the available authors from the Bookstore.",
               Tags = new List<OpenApiTag> { new() { Name = "authors" } }
           });
    }
}