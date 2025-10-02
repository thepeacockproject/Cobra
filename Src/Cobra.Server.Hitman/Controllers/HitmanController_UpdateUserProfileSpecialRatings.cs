using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Cobra.Server.Hitman.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZOnlineManager::UploadProfileSpecialRatings @ 0054AB20
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("UpdateUserProfileSpecialRatings", HttpMethods.Get, null)]
        public class UpdateUserProfileSpecialRatingsRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }

            [NormalizedJsonString]
            [SFunctionParameter("data", EdmTypes.String)]
            public UserProfileSpecialRatings Data { get; set; }
        }

        [HttpGet]
        [Route("UpdateUserProfileSpecialRatings")]
        public IActionResult UpdateUserProfileSpecialRatings([FromQuery] UpdateUserProfileSpecialRatingsRequest request)
        {
            return Ok();
        }
    }
}
