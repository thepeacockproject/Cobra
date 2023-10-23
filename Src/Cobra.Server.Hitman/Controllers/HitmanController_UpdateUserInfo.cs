using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZOnlineManager::UploadProfile @ 007ADF30
     * Callback: ZOnlineManager::OnUpdateUserProfile @ 006FB440
     * ReturnType: None, but 200 - OK.
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("UpdateUserInfo", HttpMethods.GET, null)]
        public class UpdateUserInfoRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }

            [NormalizedString]
            [SFunctionParameter("displayName", EdmTypes.String)]
            public string DisplayName { get; set; }

            [SFunctionParameter("country", EdmTypes.Int32)]
            public int Country { get; set; }

            [SplitNormalizedString]
            [SFunctionParameter("friends", EdmTypes.String)]
            public List<string> Friends { get; set; }
        }

        [HttpGet]
        [Route("UpdateUserInfo")]
        public IActionResult UpdateUserInfo([FromQuery] UpdateUserInfoRequest request)
        {
            _hitmanServer.UpdateUserInfo(request);

            return Ok();
        }
    }
}
