namespace RestWithAspNet10Scaffold.Configurations;

public static class RouteConfig
{
    public static IServiceCollection AddRoutesConfig(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options => {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        
        return services;
    }
}