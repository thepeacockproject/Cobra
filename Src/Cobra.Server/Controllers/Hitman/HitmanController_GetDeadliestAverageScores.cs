using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    /**
     * Invoke: ZLeaderboard::GetSpecialAverageDeadliest @ 00919C70
     * Callback: ZLeaderboard::GetAverageScoresCallback @ 00760350
     * ReturnType: ZOServiceOperationResult
     */
    public partial class HitmanController
    {
        [EdmFunctionImport(
            "GetDeadliestAverageScores",
            HttpMethods.GET,
            $"Collection({EdmTypes.String})"
        )]
        public class GetDeadliestAverageScoresRequest : BaseGetAverageScoresRequest
        {
            //Do nothing
        }

        [HttpGet]
        [Route("GetDeadliestAverageScores")]
        public IActionResult GetDeadliestAverageScores([FromQuery] GetDeadliestAverageScoresRequest request)
        {
            return JsonOperationListResponse(_hitmanServer.GetAverageScores(request));
        }
    }
}
