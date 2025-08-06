using Grpc.Sdk.DTOs;

namespace Clients.Mappers;

public static class BookReplyToBookRestResponses
{
    public static BookRestResponses ToBookRestResponses(this Grpc_Api.BookReply bookReply)
    {
        var bookRestResposes = new BookRestResponses
        {
            BookId = bookReply.BookId,
            Isbn = bookReply.Isbn,
            Title = bookReply.Title,
            Description = bookReply.Description,
            Authors = bookReply.Authors.Select(a => new AuthorRestResponses
            {
                AuthorId = a.AuthorId,
                FullName = a.FullName
            })
        };

        return bookRestResposes;

    }

    public static BookRestResponses ToBookRestResponses(this Grpc_Api.  CreateBookReply createBookReply)
    {
        var bookRestResposes = new BookRestResponses
        {
            BookId = createBookReply.BookId,
            Isbn = createBookReply.Isbn,
            Title = createBookReply.Title,
            Description = createBookReply.Description,
            Authors = createBookReply.Authors.Select(a => new AuthorRestResponses
            {
                AuthorId = a.AuthorId,
                FullName = a.FullName
            })
        };

        return bookRestResposes;

    }
}
