using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithAspNet10Scaffold.Hypermedia.Abstract;

public interface IResponseEnricher
{
    bool CanEnrich(ResultExecutingContext response);
    Task EnrichAsync(ResultExecutingContext context);
}