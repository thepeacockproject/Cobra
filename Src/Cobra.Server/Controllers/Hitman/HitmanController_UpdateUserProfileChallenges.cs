using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Cobra.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    /**
     * Invoke: ZOnlineManager::UploadProfileChallenges @ 008DE580
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("UpdateUserProfileChallenges", HttpMethods.GET, null)]
        public class UpdateUserProfileChallengesRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }

            [NormalizedJsonString]
            [SFunctionParameter("data", EdmTypes.String)]
            public UserProfileChallenges Data { get; set; }
        }

        [HttpGet]
        [Route("UpdateUserProfileChallenges")]
        public IActionResult UpdateUserProfileChallenges([FromQuery] UpdateUserProfileChallengesRequest request)
        {
            return Ok();
        }
    }
}
