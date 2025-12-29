using RestWithAspNet10Scaffold.Hypermedia.Enricher;
using RestWithAspNet10Scaffold.Hypermedia.Filters;

namespace RestWithAspNet10Scaffold.Configurations;

public static class HateoasConfig
{
    public static IServiceCollection AddHateoasConfiguration(this IServiceCollection services)
    {
        var filterOptions = new HypermediaFilterOptions();
        
        filterOptions.ContentResponseEnrichers.Add(
            new PersonEnricher()
            );
        
        // TODO: Add enrichers for Books

        services.AddSingleton(filterOptions);
        services.AddScoped<HypermediaFilter>();
        
        return services;
    }

    public static void UseHateoasRoutes(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllerRoute("Default", "{controller=values}/{action=get}/{id?}");
    }
}