using Scalar.AspNetCore;

namespace RestWithAspNet10Scaffold.Configurations;

public static class ScalarConfig
{
    private static readonly string AppName = "RestWithAspNet10Scaffold";
    private static readonly string AppDescription =
        "A simple API built with ASP.NET Core 10 demonstrating RESTful principles and best practices.";

    public static WebApplication UseScalarSpecification(this WebApplication app)
    {
        app.MapScalarApiReference("/scalar", options =>
        {
            options
                .WithTitle(AppName)
                .WithOpenApiRoutePattern("/swagger/V1.0.0/swagger.json");
        });
        
        return app;
    }
}