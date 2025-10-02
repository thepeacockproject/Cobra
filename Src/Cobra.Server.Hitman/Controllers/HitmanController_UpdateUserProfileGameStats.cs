using System.Text.Json;
using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZOnlineManager::UploadProfileGameStats @ 0090A2D0
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("UpdateUserProfileGameStats", HttpMethods.Get, null)]
        public class UpdateUserProfileGameStatsRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }

            [NormalizedJsonString]
            [SFunctionParameter("data", EdmTypes.String)]
            public Dictionary<string, JsonDocument> Data { get; set; }
        }

        [HttpGet]
        [Route("UpdateUserProfileGameStats")]
        public IActionResult UpdateUserProfileGameStats([FromQuery] UpdateUserProfileGameStatsRequest request)
        {
            return Ok();
        }
    }
}
