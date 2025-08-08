using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Web_Api.Data;
using Web_Api.Endpoints;
using Web_Api.Extensions;

namespace Web_Api.OpenApi.Extensions;

public static class EndpointsExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddSingleton<IBookService, BookService>();
        services.AddSingleton<IAuthorService, AuthorService>();
        services.AddSingleton<ICartService, CartService>();
        services.AddVersioning();
        AddSwagger(services);
        AddEndpointsFromAssembly(services, typeof(EndpointsExtensions).Assembly);

        return services;
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
    }

   
    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1.0))
            .HasApiVersion(new ApiVersion(2.0))
            .ReportApiVersions()
            .Build();

        RouteGroupBuilder versionedGroup = app
             .MapGroup("/api/v{apiVersion:apiVersion}")
             .WithApiVersionSet(apiVersionSet);


        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(versionedGroup);
        }

        ConfigureSwagger(app);

        return app;
    }

    private static void ConfigureSwagger(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            // app.UseSwagger(options => options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var apiVersionDescriptions = app.DescribeApiVersions();

                foreach (var apiVersionDescription in apiVersionDescriptions)
                {
                    var url = $"/swagger/{apiVersionDescription.GroupName}/swagger.json";
                    var name = apiVersionDescription.GroupName.ToUpperInvariant();

                    options.SwaggerEndpoint(url, name);
                }
            });
        }
    }

    private static void AddEndpointsFromAssembly(IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
                    .DefinedTypes
                    .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                                   type.IsAssignableTo(typeof(IEndpoint)))
                    .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                    .ToArray();

        services.TryAddEnumerable(serviceDescriptors);
    }
}
