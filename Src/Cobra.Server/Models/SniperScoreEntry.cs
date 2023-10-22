using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;

namespace Cobra.Server.Models
{
    [EdmEntity("ScoreEntry")]
    public class SniperScoreEntry : IEdmEntity
    {
        [EdmProperty("Rank", EdmTypes.Int32, false)]
        public int Rank { get; set; }

        [EdmProperty("UserId", EdmTypes.String, false)]
        public string UserId { get; set; }

        [EdmProperty("Score", EdmTypes.Int32, false)]
        public int Score { get; set; }
    }
}