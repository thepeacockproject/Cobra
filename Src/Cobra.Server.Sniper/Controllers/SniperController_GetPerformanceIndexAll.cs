using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Sniper.Controllers
{
    /**
     * Invoke: ZUnknown::GetPerformanceIndexAll @ 0080E890
     * Callback: ZUnknown::GetPerformanceIndexAllCallback @ 0080E4B0
     * ReturnType: ZOServiceOperationResult
     */
    public partial class SniperController
    {
        [EdmFunctionImport("GetPerformanceIndexAll", HttpMethods.GET, null)]
        public class GetPerformanceIndexAllRequest : IEdmFunctionImport
        {
            [SFunctionParameter("leaderboardid", EdmTypes.Int32)]
            public int LeaderboardId { get; set; }

            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }
        }

        [HttpGet]
        [Route("GetPerformanceIndexAll")]
        public IActionResult GetPerformanceIndexAll([FromQuery] GetPerformanceIndexAllRequest request)
        {
            //NOTE: Different performance percentages are at: 0, 0.55, 0.65, 0.75 and 1
            return JsonOperationListResponse(_sniperServer.GetPerformanceIndexAll(request));
        }
    }
}
