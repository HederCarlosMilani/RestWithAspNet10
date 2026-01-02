using RestWithAspNet10Scaffold.Mail.Settings;

namespace RestWithAspNet10Scaffold.Configurations;

public static class EmailConfig
{
    public static IServiceCollection AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("Email");
        var configs = section.Get<EmailSettings>();
        
        // Replace environment variables in configuration
        var emailUsername = Environment.GetEnvironmentVariable("EMAIL_USERNAME");
        var emailPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

        if (!string.IsNullOrEmpty(emailUsername))
        {
            configs.Username = emailUsername;
            configs.From = emailUsername;
        }
        if (!string.IsNullOrEmpty(emailPassword))
            configs.Password = emailPassword;
        
        if (configs == null)
            throw new ArgumentNullException(nameof(configs), "Email settings configuration is missing or invalid.");
        
        services.AddSingleton(configs);

        return services;
    }
}