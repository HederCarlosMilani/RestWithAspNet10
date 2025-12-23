using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithAspNet10Scaffold.Hypermedia.Abstract;

public interface IResponseEnricher
{
    bool CanEnrich(ResultExecutedContext context);
    Task EnrichAsync(ResultExecutedContext context);
}