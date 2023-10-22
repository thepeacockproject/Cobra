using System.Text.Json;
using System.Text.Json.Serialization;
using Nullable = Cobra.Server.Enums.Nullable;

namespace Cobra.Server.Json
{
    public class BooleanToStringConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value ? Nullable.True : Nullable.False);
        }
    }
}
