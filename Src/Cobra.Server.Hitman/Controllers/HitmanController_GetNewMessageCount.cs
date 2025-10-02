using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZMessageManager::Update @ 0099DFA0
     * Callback: ZMessageManager::OnGetNewMessageCountComplete @ 008847F0
     * ReturnType: ZOServiceOperationResult
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("GetNewMessageCount", HttpMethods.Get, null)]
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
            return JsonOperationValueResponse(_hitmanServer.GetNewMessageCount(request));
        }
    }
}
