using Grpc_Api;

namespace Grpc.Sdk;

public interface IBookGrpcService
{
    Task<AllBookReply> GetBooks(AllBookRequest request, CancellationToken cancellationToken);
    Task<BookReply> GetBookById(BookRequest request, CancellationToken cancellationToken);
    Task<CreateBookReply> CreateBook(CreateBookRequest request, CancellationToken cancellationToken);
    Task<BookReply> DeleteBookById(BookRequest request, CancellationToken cancellationToken);
}