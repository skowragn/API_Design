using Grpc.Core;

namespace Grpc_Api.Services;

public class AuthorService : Author.AuthorBase
{
    private readonly ILogger<BookService> _logger;
    private static readonly List<AuthorReply> _authors = [];
    private static readonly AllAuthorReply _allAuthors = new();

    public AuthorService(ILogger<BookService> logger)
    {
        _logger = logger;
        AuthorsStorageInit();
    }

    public override Task<AllAuthorReply> GetAuthors(AllAuthorRequest request, ServerCallContext context)
    {
        return Task.FromResult(_allAuthors);
    }

    private static void AuthorsStorageInit()
    {
        _authors.Add(new AuthorReply()
        {
            AuthorId = 1,
            FullName = "Author 1"
        });

        _authors.Add(new AuthorReply()
        {
            AuthorId = 2,
            FullName = "Author 2"
        });

        _authors.Add(new AuthorReply()
        {
            AuthorId = 3,
            FullName = "Author 3"
        });

        _allAuthors?.Authors.Add(_authors);
    }
}