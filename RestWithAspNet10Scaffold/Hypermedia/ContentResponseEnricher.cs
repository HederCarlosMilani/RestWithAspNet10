using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestWithAspNet10Scaffold.Hypermedia.Abstract;

namespace RestWithAspNet10Scaffold.Hypermedia;

public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportsHypermidea
{
    public virtual bool CanEnrich(Type contentType)
    {
        return contentType == typeof(T) || contentType == typeof(List<T>);
    }
    
    protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);
    
    bool IResponseEnricher.CanEnrich(ResultExecutingContext response)
    {
        if (response.Result is OkObjectResult okObjectResult)
        {
            return CanEnrich(okObjectResult.Value.GetType());
        }
        return false;
    }

    public async Task EnrichAsync(ResultExecutingContext response)
    {
        var urlHelper = new UrlHelperFactory().GetUrlHelper(response);

        if (response.Result is OkObjectResult okObjectResult)
        {
            if (okObjectResult.Value is T model)
            {
                await EnrichModel(model, urlHelper);
            }
            else if (okObjectResult.Value is List<T> collection)
            {
                foreach (var item in collection)
                {
                    await EnrichModel(item, urlHelper);
                }
            }
        }
        
        await Task.CompletedTask;
    }
}