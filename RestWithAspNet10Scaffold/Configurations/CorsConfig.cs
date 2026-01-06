namespace RestWithAspNet10Scaffold.Configurations;

public static class CorsConfig
{
    public static IServiceCollection AddCorsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();
        services.AddCors(options =>
        {
            options.AddPolicy("LocalPolicy", policyBuilder =>
            {
                policyBuilder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
            options.AddPolicy("DefaultPolicy", policyBuilder =>
            {
                policyBuilder.WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        
        return services;
    }
    
    public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder app)
    {
        app.UseCors("DefaultPolicy");
        return app;
    }
}