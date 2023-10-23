using System.Text.Json.Serialization;

namespace Cobra.Server.Models.Response
{
    public class EntryObject<T>
    {
        [JsonPropertyName("results")]
        public T Results { get; set; }
    }
}
