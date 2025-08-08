using Clients.Extensions;
using Grpc.Sdk;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcSdk();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddVersioning();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.AddSwaggerUIConfiguration();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();