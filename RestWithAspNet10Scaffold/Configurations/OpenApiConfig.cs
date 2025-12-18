using Microsoft.OpenApi;

namespace RestWithAspNet10Scaffold.Configurations;

public static class OpenApiConfig
{
    private static readonly string AppName = "RestWithAspNet10Scaffold";

    private static readonly string AppDescription =
        "A simple API built with ASP.NET Core 10 demonstrating RESTful principles and best practices.";

    public static IServiceCollection AddOpenApiConfig(this IServiceCollection services)
    {
        services.AddSingleton(new OpenApiInfo
        {
            Title = AppName,
            Version = "v1",
            Description = AppDescription,
            Contact = new OpenApiContact
            {
                Name = "Heder Milani",
                Email = "hedermilani@hotmail.com"
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        });
        return services;
    }
}