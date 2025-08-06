using Asp.Versioning;
using Clients.Mappers;
using Grpc.Sdk;
using Grpc.Sdk.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Clients.Controllers;

[ApiVersion(1)]
[Route("api/v{version:apiVersion}/grpc/authors")]
[ApiController]
public class AuthorGrpcClientController : ControllerBase
{
    private readonly IAuthorGrpcService _grpcAuthorService;
    public AuthorGrpcClientController(IAuthorGrpcService grpcAuthorService)
    {
        _grpcAuthorService = grpcAuthorService;
    }

    // GET: authors 200
    [MapToApiVersion(1)]
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuthorRestResponses>))]
    public async Task<ActionResult<IEnumerable<AuthorRestResponses>>> Get()
    {
        var authorResults = await _grpcAuthorService.GetAuthors(new Grpc_Api.AllAuthorRequest(), CancellationToken.None);

        if (authorResults == null)
        {
            return NotFound("Book not found");
        }

        var authors = authorResults.Authors.Select(b => b.ToAuthorRestResponses()).ToList();

        return Ok(authors);
    }
}