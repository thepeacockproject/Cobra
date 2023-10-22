using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Sniper
{
    /**
     * Invoke: ZLeaderboard::UploadScore @ 0080DE00
     * Callback: None
     */
    public partial class SniperController
    {
        [EdmFunctionImport("PutScore", HttpMethods.GET, null)]
        public class PutScoreRequest : IEdmFunctionImport
        {
            [SFunctionParameter("leaderboardid", EdmTypes.Int32)]
            public int LeaderboardId { get; set; }

            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }

            [SFunctionParameter("score", EdmTypes.Int32)]
            public int Score { get; set; }
        }

        [HttpGet]
        [Route("PutScore")]
        public IActionResult PutScore([FromQuery] PutScoreRequest request)
        {
            return Ok();
        }
    }
}
