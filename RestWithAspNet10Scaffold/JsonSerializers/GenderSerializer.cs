using System.Text.Json;
using System.Text.Json.Serialization;

namespace RestWithAspNet10Scaffold.JsonSerializers;

public class GenderSerializer : JsonConverter<String>
{
    public override string? Read(
        ref Utf8JsonReader reader, 
        Type typeToConvert, 
        JsonSerializerOptions options
        ) => reader.GetString();

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        var formattedGender = value.ToLower() == "male" ? "M" : "F";
        writer.WriteStringValue(formattedGender);
    }
}