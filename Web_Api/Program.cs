using Web_Api.OpenApi.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpoints();

var app = builder.Build();

app.MapEndpoints();

app.UseHttpsRedirection();


app.Run();
