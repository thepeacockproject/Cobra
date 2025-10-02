using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZLeaderboard::GetSpecialDeadliest @ 00791C00
     * Callback: ZLeaderboard::GetScoresCallback @ 007FD3D0
     * ReturnType: ZOFeed
     */
    public partial class HitmanController
    {
        [EdmFunctionImport(
            "GetDeadliestScores",
            HttpMethods.Get,
            $"Collection({SchemaNamespace}.ScoreEntry)"
        )]
        public class GetDeadliestScoresRequest : BaseGetScoresRequest
        {
            //Do nothing
        }

        [HttpGet]
        [Route("GetDeadliestScores")]
        public IActionResult GetDeadliestScores([FromQuery] GetDeadliestScoresRequest request)
        {
            return JsonFeedResponse(_hitmanServer.GetScores(request));
        }
    }
}
