using Microsoft.OpenApi;

namespace RestWithAspNet10Scaffold.Configurations;

public static class SwaggerConfig
{
    private static readonly string AppName = "RestWithAspNet10Scaffold";
    private static readonly string AppDescription =
        "A simple API built with ASP.NET Core 10 demonstrating RESTful principles and best practices.";

    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("V1.0.0", new OpenApiInfo
            {
                Title = AppName,
                Version = "V1.0.0",
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
            
            c.CustomSchemaIds(x => x.FullName);
        });
        
        return services;
    }

    public static IApplicationBuilder UseSwaggerSpecification(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/V1.0.0/swagger.json", "RestWithAspNet10Scaffold V1.0.0");
            c.RoutePrefix = "swagger-ui";
            c.DocumentTitle = AppName;
        });
        
        return app;
    }
}