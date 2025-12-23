namespace RestWithAspNet10Scaffold.Hypermedia.Abstract;

public interface ISupportsHypermidea
{
    List<HypermideaLink> Links { get; set; }
}