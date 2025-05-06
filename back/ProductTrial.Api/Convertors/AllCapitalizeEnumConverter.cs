using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProductTrial.Api.Convertors
{
    public class AllCapitalizeEnumConverter : JsonConverter<Enum>
    {
        public override Enum Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            return (Enum)Enum.Parse(type, reader.GetString(), ignoreCase: true);
        }

        public override void Write(Utf8JsonWriter writer, Enum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().ToUpper());
        }
    }
}