using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Cobra.Server.Hitman.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZOnlineManager::UploadProfileChallenges @ 008DE580
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("UpdateUserProfileChallenges", HttpMethods.Get, null)]
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
