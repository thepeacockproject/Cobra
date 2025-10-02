using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Sniper.Controllers
{
    /**
     * Invoke: ZLeaderboard::GetScores @ 0080E630
     * Callback: ZLeaderboard::GetScoresCallback @ 0080DF80
     * ReturnType: ZOFeed
     */
    public partial class SniperController
    {
        [EdmFunctionImport(
            "GetScores",
            HttpMethods.Get,
            $"Collection({SchemaNamespace}.ScoreEntry)"
        )]
        public class GetScoresRequest : IEdmFunctionImport
        {
            [SFunctionParameter("leaderboardid", EdmTypes.Int32)]
            public int LeaderboardId { get; set; }

            [SFunctionParameter("filter", EdmTypes.Int32)]
            public int Filter { get; set; }

            [SFunctionParameter("startindex", EdmTypes.Int32)]
            public int StartIndex { get; set; }

            [SFunctionParameter("range", EdmTypes.Int32)]
            public int Range { get; set; }

            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }
        }

        [HttpGet]
        [Route("GetScores")]
        public IActionResult GetScores([FromQuery] GetScoresRequest request)
        {
            //NOTE: leaderboardid is always 0
            return JsonFeedResponse(_sniperServer.GetScores(request));
        }
    }
}
