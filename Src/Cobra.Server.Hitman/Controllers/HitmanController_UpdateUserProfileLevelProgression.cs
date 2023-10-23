using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Cobra.Server.Hitman.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZOnlineManager::UploadProfileLevelProgression @ 004613F0
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("UpdateUserProfileLevelProgression", HttpMethods.GET, null)]
        public class UpdateUserProfileLevelProgressionRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }

            [NormalizedJsonString]
            [SFunctionParameter("data", EdmTypes.String)]
            public UserProfileLevelProgression Data { get; set; }
        }

        [HttpGet]
        [Route("UpdateUserProfileLevelProgression")]
        public IActionResult UpdateUserProfileLevelProgression([FromQuery] UpdateUserProfileLevelProgressionRequest request)
        {
            return Ok();
        }
    }
}
