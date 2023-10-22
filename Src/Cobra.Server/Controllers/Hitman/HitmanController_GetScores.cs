using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    /**
     * Invoke: ZLeaderboard::GetScores @ 007FA160
     * Callback: ZLeaderboard::GetScoresCallback @ 007FD3D0
     * ReturnType: ZOFeed
     */
    public partial class HitmanController
    {
        [EdmFunctionImport(
            "GetScores",
            HttpMethods.GET,
            $"Collection({SchemaNamespace}.ScoreEntry)"
        )]
        public class GetScoresRequest : BaseGetScoresRequest
        {
            [SFunctionParameter("leaderboardtype", EdmTypes.Int32)]
            public int LeaderboardType { get; set; }

            [NormalizedString]
            [SFunctionParameter("leaderboardid", EdmTypes.String)]
            public string LeaderboardId { get; set; }
        }

        [HttpGet]
        [Route("GetScores")]
        public IActionResult GetScores([FromQuery] GetScoresRequest request)
        {
            return JsonFeedResponse(_hitmanServer.GetScores(request));
        }
    }
}