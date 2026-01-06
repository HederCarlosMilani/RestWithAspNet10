using System.Text.Json.Serialization;
using RestWithAspNet10Scaffold.Hypermedia;
using RestWithAspNet10Scaffold.Hypermedia.Abstract;
using RestWithAspNet10Scaffold.JsonSerializers;

namespace RestWithAspNet10Scaffold.Data.Dto.V1;

public class PersonDto : ISupportsHypermidea
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Gender {get; set;}
    public bool Enabled { get; set; }
    public List<HypermideaLink> Links { get; set; } = [];
}