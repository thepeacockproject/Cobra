using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cobra.Server.Edm.Json
{
    public class AnyToJsonStringConverter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            return value == null
                ? default
                : JsonSerializer.Deserialize<T>(value);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            //NOTE: options is purposely not passed on to the Serialize method, since the in-game JSON parser used to Deserialize this actually works fine with the default options.
            writer.WriteStringValue(JsonSerializer.Serialize(value));
        }
    }
}
