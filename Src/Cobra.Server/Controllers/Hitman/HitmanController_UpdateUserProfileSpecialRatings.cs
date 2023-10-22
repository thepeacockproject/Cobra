using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Cobra.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    /**
     * Invoke: ZOnlineManager::UploadProfileSpecialRatings @ 0054AB20
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("UpdateUserProfileSpecialRatings", HttpMethods.GET, null)]
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
