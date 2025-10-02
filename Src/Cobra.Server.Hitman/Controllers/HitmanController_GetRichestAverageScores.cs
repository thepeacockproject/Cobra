using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
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
            HttpMethods.Get,
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
