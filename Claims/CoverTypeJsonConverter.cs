using System.Text.Json;
using System.Text.Json.Serialization;

namespace Claims.Interchange
{
    public class CoverTypeJsonConverter : JsonConverter<CoverType>
    {
        public override CoverType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return (CoverType)Enum.Parse(typeof(CoverType), value, ignoreCase: true);
        }

        public override void Write(Utf8JsonWriter writer, CoverType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
