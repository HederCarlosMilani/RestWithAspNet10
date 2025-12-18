using System.Text.Json.Serialization;
using RestWithAspNet10Scaffold.JsonSerializers;

namespace RestWithAspNet10Scaffold.Data.Dto.V2;

public class PersonDto
{
    [JsonPropertyName("code")]
    public long Id { get; set; }
    
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("last_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string LastName { get; set; }
    
    [JsonIgnore]
    public string Address { get; set; }
    
    [JsonConverter(typeof(GenderSerializer))]
    public string Gender {get; set;}
    
    [JsonConverter(typeof(DateSerializer))]
    public  DateTime? BirthDate { get; set; }
}