using System.Text.Json.Serialization;
using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Cobra.Server.Shared.Enums;

namespace Cobra.Server.Sniper.Models
{
    [EdmEntity("Message")]
    public class Message : IEdmEntity
    {
        //NOTE: Even though this is treated as a String in-game, we always assign an Integer.
        [JsonPropertyName("_id")]
        [EdmProperty("_id", EdmTypes.String, false)]
        public int Id { get; set; }

        [EdmProperty("FromId", EdmTypes.String, false)]
        public string FromId { get; set; }

        [EdmProperty("Category", EdmTypes.Int32, false)]
        public EMessageCategory Category { get; set; }

        [EdmProperty("TimestampUTC", EdmTypes.Int64, false)]
        public long TimestampUTC { get; set; }

        //https://youtrack.jetbrains.com/issue/RSRP-489484
        //ReSharper disable once InconsistentNaming
        [EdmProperty("TextTemplateId", EdmTypes.Int32, false)]
        public int TextTemplateId { get; set; }

        [EdmProperty("TemplateData", EdmTypes.String, false)]
        public string TemplateData { get; set; }
    }
}
