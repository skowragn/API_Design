using Grpc_Api.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc(options =>
{
   options.EnableDetailedErrors = true;
});

var app = builder.Build();

app.MapGrpcService<AuthorService>();
app.MapGrpcService<BookService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. " +
"                     To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
