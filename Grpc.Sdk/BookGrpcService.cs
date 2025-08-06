using Grpc.Core;
using Grpc_Api;

namespace Grpc.Sdk;

public class BookGrpcService : IBookGrpcService
{
    private readonly Book.BookClient _grpcClient;
    public BookGrpcService(Book.BookClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    public async Task<AllBookReply> GetBooks(AllBookRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var newRequest = new AllBookRequest();
            var response = _grpcClient.GetBooks(newRequest, cancellationToken: cancellationToken);
            return response;
        }
        catch (RpcException)
        {
            throw;
        }
    }
    public async Task<BookReply> GetBookById(BookRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var newRequest = new BookRequest { BookId = 1 };
            var response = _grpcClient.GetBookById(newRequest, cancellationToken: cancellationToken);
            return response;
        }
        catch (RpcException)
        {
            throw;
        }
    }

    public async Task<CreateBookReply> CreateBook(CreateBookRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = _grpcClient.CreateBook(request, cancellationToken: cancellationToken);
            return response;
        }
        catch (RpcException)
        {
            throw;
        }
    }

    public async Task<BookReply> DeleteBookById(BookRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = _grpcClient.DeleteBookById(request, cancellationToken: cancellationToken);
            return response;
        }
        catch (RpcException)
        {
            throw;
        }
    }
}
