using Microsoft.EntityFrameworkCore;

namespace RestWithAspNet10Scaffold.Configurations;

public static class DatabaseConfig
{
    public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["MSSQLServerConnection:MSSQLServerConnectionString"];
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'MSSQLServerConnectionString' not found.");
        }
        
        services.AddDbContext<Context.MSSQLContext>(options => options.UseSqlServer(connectionString));
        return services;
    }
}