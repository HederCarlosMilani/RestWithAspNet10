using RestWithAspNet10Scaffold.Hypermedia.Abstract;

namespace RestWithAspNet10Scaffold.Hypermedia.Filters;

public class HypermediaFilterOptions
{
    public List<IResponseEnricher> ContentResponseEnrichers { get; set; } = [];
}