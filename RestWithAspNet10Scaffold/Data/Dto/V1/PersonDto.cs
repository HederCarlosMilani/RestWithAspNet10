using System.Text.Json.Serialization;

namespace RestWithAspNet10Scaffold.Data.Dto.V1;

public class PersonDto
{
    [JsonPropertyName("code")]
    public long Id { get; set; }
    
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Gender {get; set;}
}