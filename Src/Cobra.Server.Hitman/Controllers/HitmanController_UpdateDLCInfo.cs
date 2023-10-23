using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZOnlineManager::UploadDLCToken @ 0042AA0
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("UpdateDLCInfo", HttpMethods.GET, null)]
        public class UpdateDLCInfoRequest : IEdmFunctionImport
        {
            [SplitNormalizedString]
            [SFunctionParameter("dlctokens", EdmTypes.String)]
            public List<string> DLCTokens { get; set; }

            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }
        }

        [HttpGet]
        [Route("UpdateDLCInfo")]
        public IActionResult UpdateDLCInfo([FromQuery] UpdateDLCInfoRequest request)
        {
            return Ok();
        }
    }
}
