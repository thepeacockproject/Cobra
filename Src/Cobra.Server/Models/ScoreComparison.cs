using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;

namespace Cobra.Server.Models
{
    [EdmEntity("ScoreComparison")]
    public class ScoreComparison : IEdmEntity
    {
        [EdmProperty("FriendName", EdmTypes.String, false)]
        public string FriendName { get; set; }

        [EdmProperty("FriendScore", EdmTypes.Int32, false)]
        public int FriendScore { get; set; }

        [EdmProperty("CountryAverage", EdmTypes.Int32, false)]
        public int CountryAverage { get; set; }

        [EdmProperty("WorldAverage", EdmTypes.Int32, false)]
        public int WorldAverage { get; set; }
    }
}
