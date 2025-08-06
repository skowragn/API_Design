using Grpc.Core;
using Grpc_Api;


namespace Grpc.Sdk;

public class AuthorGrpcService : IAuthorGrpcService
{
    private readonly Author.AuthorClient _grpcClient;
    public AuthorGrpcService(Author.AuthorClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

     public async Task<AllAuthorReply> GetAuthors(AllAuthorRequest request, CancellationToken cancellationToken)
     {
         try
         {
             var newRequest = new AllAuthorRequest();
             var response = _grpcClient.GetAuthors(newRequest, cancellationToken: cancellationToken);
             return response;
         }
         catch (RpcException)
         {
             throw;
         }
     }
}
