using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    /**
     * Invoke: ZLeaderboard::GetSpecialAverageRichest @ 0044D220
     * Callback: ZLeaderboard::GetAverageScoresCallback @ 00760350
     * ReturnType: ZOServiceOperationResult
     */
    public partial class HitmanController
    {
        [EdmFunctionImport(
            "GetRichestAverageScores",
            HttpMethods.GET,
            $"Collection({EdmTypes.String})"
        )]
        public class GetRichestAverageScoresRequest : BaseGetAverageScoresRequest
        {
            //Do nothing
        }

        [HttpGet]
        [Route("GetRichestAverageScores")]
        public IActionResult GetRichestAverageScores([FromQuery] GetRichestAverageScoresRequest request)
        {
            return JsonOperationListResponse(_hitmanServer.GetAverageScores(request));
        }
    }
}
