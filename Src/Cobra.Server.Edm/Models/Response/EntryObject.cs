using System.Text.Json.Serialization;

namespace Cobra.Server.Edm.Models.Response
{
    public class EntryObject<T>
    {
        [JsonPropertyName("results")]
        public T Results { get; set; }
    }
}
