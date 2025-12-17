using EvolveDb;
using Microsoft.Data.SqlClient;
using Serilog;

namespace RestWithAspNet10Scaffold.Configurations;

public static class EvolveConfig
{
    public static IServiceCollection AddEvolveConfig(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            var connectionString = configuration["MSSQLServerConnection:MSSQLServerConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'MSSQLServerConnectionString' not found.");
            }

            try
            {
                using var evolveConnection = new SqlConnection(connectionString);

                var evolve = new Evolve(
                    evolveConnection,
                    msg => Log.Information(msg)
                )
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true
                };
                
                evolve.Migrate();
            }
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while migrating the database.");
                throw;
            }
        }
        return services;
    }
}