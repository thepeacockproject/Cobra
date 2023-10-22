using System.Text.Json.Serialization;
using Cobra.Server.Json;

namespace Cobra.Server.Models
{
    public class Competition
    {
        //NOTE: Has to be converted to a String, otherwise the game will crash.
        [JsonConverter(typeof(IntegerToStringConverter))]
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        public DateTime EndTimeUTC { get; set; }
        public bool AllowInvites { get; set; }
        public string CompetitionCreator { get; set; }
        public int DaysRemaining { get; set; }
    }
}