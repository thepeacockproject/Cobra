using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Sniper.Controllers
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
            return JsonOperationValueResponse(_sniperServer.GetNewMessageCount(request));
        }
    }
}
