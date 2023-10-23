using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;

namespace Cobra.Server.Hitman.Models
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
