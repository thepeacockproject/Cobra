using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Sniper
{
    /**
     * Invoke: ZMessageManager::Update @ 00801320
     * Callback: ZMessageManager::OnGetNewMessageCountComplete @ 008011F0
     * ReturnType: ZOServiceOperationResult
     */
    public partial class SniperController
    {
        [EdmFunctionImport("GetNewMessageCount", HttpMethods.GET, null)]
        public class GetNewMessageCountRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("userId", EdmTypes.String)]
            public string UserId { get; set; }
        }

        [HttpGet]
        [Route("GetNewMessageCount")]
        public IActionResult GetNewMessageCount([FromQuery] GetNewMessageCountRequest request)
        {
            //NOTE: GetNewMessageCount doesn't care for the name of the Key
            return JsonOperationValueResponse(10);
        }
    }
}
