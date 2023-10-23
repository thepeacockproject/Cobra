using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;

namespace Cobra.Server.Hitman.Models
{
    [EdmEntity("ScoreEntry")]
    public class ScoreEntry : IEdmEntity
    {
        [EdmProperty("Rank", EdmTypes.Int32, false)]
        public int Rank { get; set; }

        [EdmProperty("UserId", EdmTypes.String, false)]
        public string UserId { get; set; }

        [EdmProperty("Score", EdmTypes.Int32, false)]
        public int Score { get; set; }

        [EdmProperty("Rating", EdmTypes.Int32, false)]
        public int Rating { get; set; }
    }
}
