using Microsoft.Extensions.DependencyInjection;
using Grpc_Api;
using Grpc.Sdk.Interfaces;
using Grpc.Sdk.Services;


namespace Grpc.Sdk;

public static class ServiceCollectionExtension
{
    public static void AddGrpcSdk(this IServiceCollection services)
    { 
         services.AddGrpcClient<Book.BookClient>(client =>
         {
             client.Address = new Uri("https://localhost:7067");
         });

        services.AddScoped<IBookGrpcService, BookGrpcService>();

        services.AddGrpcClient<Author.AuthorClient>(client =>
        {
            client.Address = new Uri("https://localhost:7067");
        });

        services.AddScoped<IAuthorGrpcService, AuthorGrpcService>();

        services.AddGrpcClient<Cart.CartClient>(client =>
        {
            client.Address = new Uri("https://localhost:7067");
        });

        services.AddScoped<ICartGrpcService, CartGrpcService>();
    }

}
