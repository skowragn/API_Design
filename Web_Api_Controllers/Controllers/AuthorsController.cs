using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Web_Api_Controllers.Data;
using Web_Api_Controllers.DTOs.Authors;

namespace Web_Api_Controllers.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        // GET: authors 200
        [MapToApiVersion(1)]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDto))]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> Get()
        {
            var authors = await MockStorage.GetAuthorList();
            return Ok(authors);
        }
    }
}
