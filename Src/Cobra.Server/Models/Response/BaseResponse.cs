using System.Text.Json.Serialization;

namespace Cobra.Server.Models.Response
{
    public class BaseResponse<T>
    {
        [JsonPropertyName("d")]
        public T Data { get; set; }
    }
}
