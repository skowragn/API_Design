using Grpc_Api;

namespace Grpc.Sdk.Interfaces;

public interface IAuthorGrpcService
{
   Task<AllAuthorReply> GetAuthors(AllAuthorRequest request, CancellationToken cancellationToken);
}
