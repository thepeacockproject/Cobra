using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZLeaderboard::GetSpecialPopular @ 0095F6A0
     * Callback: ZLeaderboard::GetScoresCallback @ 007FD3D0
     * ReturnType: ZOFeed
     */
    public partial class HitmanController
    {
        [EdmFunctionImport(
            "GetPopularScores",
            HttpMethods.GET,
            $"Collection({SchemaNamespace}.ScoreEntry)"
        )]
        public class GetPopularScoresRequest : BaseGetScoresRequest
        {
            //Do nothing
        }

        [HttpGet]
        [Route("GetPopularScores")]
        public IActionResult GetPopularScores([FromQuery] GetPopularScoresRequest request)
        {
            return JsonFeedResponse(_hitmanServer.GetScores(request));
        }
    }
}
