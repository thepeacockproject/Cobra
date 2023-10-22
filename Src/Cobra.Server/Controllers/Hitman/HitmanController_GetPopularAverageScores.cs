using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    /**
     * Invoke: ZLeaderboard::GetSpecialAveragePopular @ 00895B20
     * Callback: ZLeaderboard::GetAverageScoresCallback @ 00760350
     * ReturnType: ZOServiceOperationResult
     */
    public partial class HitmanController
    {
        [EdmFunctionImport(
            "GetPopularAverageScores",
            HttpMethods.GET,
            $"Collection({EdmTypes.String})"
        )]
        public class GetPopularAverageScoresRequest : BaseGetAverageScoresRequest
        {
            //Do nothing
        }

        [HttpGet]
        [Route("GetPopularAverageScores")]
        public IActionResult GetPopularAverageScores([FromQuery] GetPopularAverageScoresRequest request)
        {
            return JsonOperationListResponse(_hitmanServer.GetAverageScores(request));
        }
    }
}
