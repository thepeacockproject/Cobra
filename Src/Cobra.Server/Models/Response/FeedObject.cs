using System.Text.Json.Serialization;

namespace Cobra.Server.Models.Response
{
    public class FeedObject<T>
    {
        [JsonPropertyName("results")]
        public List<T> Results { get; set; }

        [JsonPropertyName("__count")]
        public int Count { get; set; }
    }
}