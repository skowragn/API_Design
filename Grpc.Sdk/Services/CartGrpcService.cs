using Grpc.Core;
using Grpc.Sdk.Interfaces;
using Grpc_Api;
using System.Threading;

namespace Grpc.Sdk.Services;

public class CartGrpcService : ICartGrpcService
{
    private readonly Cart.CartClient _grpcClient;
    public CartGrpcService(Cart.CartClient grpcClient)
    {
        _grpcClient = grpcClient;
    }
    public async Task<CartReply> GetCartById(CartRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = _grpcClient.GetCartById(request, cancellationToken: cancellationToken);
            return response;
        }
        catch (RpcException)
        {
            throw;
        }
    }

    public async Task<CreateCartReply> CreateCart(CreateCartRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = _grpcClient.CreateCart(request, cancellationToken: cancellationToken);
            return response;
        }
        catch (RpcException)
        {
            throw;
        }
    }

    public async Task<CartReply> DeleteCartById(CartRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = _grpcClient.DeleteCartById(request, cancellationToken: cancellationToken);
            return response;
        }
        catch (RpcException)
        {
            throw;
        }
    }

    public async Task<CartReply> DeleteCartItemById(DeleteCartItemRequest cartRequest, CancellationToken cancellationToken)
    {
        try
        {
            var cart = _grpcClient.DeleteCartItemByIndex(cartRequest, cancellationToken: cancellationToken);
            return cart;

        }
        catch (RpcException)
        {
            throw;
        }
    }
}
