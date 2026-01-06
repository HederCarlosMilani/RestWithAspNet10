using System.Text.Json;
using System.Text.Json.Serialization;

namespace RestWithAspNet10Scaffold.JsonSerializers;

public class DateSerializer : JsonConverter<DateTime?>
{
    private const string _format = "dd/MM/yyyy";
    
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateTime.TryParseExact(reader.GetString(),
                _format,
                null,
                System.Globalization.DateTimeStyles.RoundtripKind,
                out var date))
        {
            return date;
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString(_format));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}