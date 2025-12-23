using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Hypermedia.Constants;

namespace RestWithAspNet10Scaffold.Hypermedia.Enricher;

public class PersonEnricher : ContentResponseEnricher<PersonDto>
{
    protected override Task EnrichModel(PersonDto content, IUrlHelper urlHelper)
    {
        var request = urlHelper.ActionContext.HttpContext.Request;
        var baseUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}/person";
        content.Links.AddRange(GenerateLinks(content.Id, baseUrl));
        
        return Task.CompletedTask;
    }

    private IEnumerable<HypermideaLink> GenerateLinks(long contentId, string baseUrl)
    {
        return (List<HypermideaLink>)
        [
            new()
            {
                Rel = RelationType.SELF,
                Href = $"{baseUrl}/{contentId}",
                Type = ResponseTypeFormat.DefaultGet,
                Action = HttpActionVerb.GET
            },

            new()
            {
                Rel = RelationType.COLLECTION,
                Href = $"{baseUrl}",
                Type = ResponseTypeFormat.DefaultGet,
                Action = HttpActionVerb.GET
            },

            new()
            {
                Rel = RelationType.CREATE,
                Href = $"{baseUrl}",
                Type = ResponseTypeFormat.DefaultPost,
                Action = HttpActionVerb.POST
            },

            new()
            {
                Rel = RelationType.UPDATE,
                Href = $"{baseUrl}",
                Type = ResponseTypeFormat.DefaultPost,
                Action = HttpActionVerb.PUT
            },

            new()
            {
                Rel = RelationType.ENABLE,
                Href = $"{baseUrl}/enable/{contentId}",
                Type = ResponseTypeFormat.DefaultPathch,
                Action = HttpActionVerb.PATCH
            },

            new()
            {
                Rel = RelationType.DISABLE,
                Href = $"{baseUrl}/disable/{contentId}",
                Type = ResponseTypeFormat.DefaultPathch,
                Action = HttpActionVerb.PATCH
            },

            new()
            {
                Rel = RelationType.DELETE,
                Href = $"{baseUrl}/{contentId}",
                Type = ResponseTypeFormat.DefaultDelete,
                Action = HttpActionVerb.DELETE
            }
        ];
    }
}