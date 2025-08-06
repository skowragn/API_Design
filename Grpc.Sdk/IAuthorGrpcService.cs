using Grpc_Api;

namespace Grpc.Sdk;

public interface IAuthorGrpcService
{
   Task<AllAuthorReply> GetAuthors(AllAuthorRequest request, CancellationToken cancellationToken);
}
