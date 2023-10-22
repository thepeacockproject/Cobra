﻿using System.Text.Json.Serialization;
using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;

namespace Cobra.Server.Models
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

        [EdmProperty("IsRead", EdmTypes.Boolean, false)]
        public bool IsRead { get; set; }

        //ReSharper disable once InconsistentNaming
        //NOTE: Due to weird bug in Rider?
        [EdmProperty("TextTemplateId", EdmTypes.Int32, false)]
        public EMessageTextTemplate TextTemplateId { get; set; }

        [EdmProperty("TemplateData", EdmTypes.String, false)]
        public string TemplateData { get; set; }
    }
}