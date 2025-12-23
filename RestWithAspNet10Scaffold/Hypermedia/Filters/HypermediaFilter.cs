using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithAspNet10Scaffold.Hypermedia.Filters;

public class HypermediaFilter(HypermediaFilterOptions hypermediaFilterOptions) : ResultFilterAttribute
{
    private readonly HypermediaFilterOptions _hypermediaFilterOptions = hypermediaFilterOptions;
    
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        TryEnrichResult(context);
        base.OnResultExecuting(context);
    }

    private void TryEnrichResult(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var enricher = _hypermediaFilterOptions
                .ContentResponseEnrichers
                .FirstOrDefault(option => option.CanEnrich(context));
            enricher?.EnrichAsync(context).Wait();
        }
    }
}