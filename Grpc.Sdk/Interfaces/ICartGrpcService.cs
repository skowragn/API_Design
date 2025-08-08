using Grpc_Api;

namespace Grpc.Sdk.Interfaces;

public interface ICartGrpcService
{
    Task<CartReply> GetCartById(CartRequest request, CancellationToken cancellationToken);
    Task<CreateCartReply> CreateCart(CreateCartRequest request, CancellationToken cancellationToken);
    Task<CartReply> DeleteCartById(CartRequest request, CancellationToken cancellationToken);
    Task<CartReply> DeleteCartItemById(DeleteCartItemRequest cartRequest, CancellationToken cancellationToken);
}