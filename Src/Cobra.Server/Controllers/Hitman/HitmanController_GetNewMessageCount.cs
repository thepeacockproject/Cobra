using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    /**
     * Invoke: ZMessageManager::Update @ 0099DFA0
     * Callback: ZMessageManager::OnGetNewMessageCountComplete @ 008847F0
     * ReturnType: ZOServiceOperationResult
     */
    public partial class HitmanController
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
            return JsonOperationValueResponse(_hitmanServer.GetNewMessageCount(request));
        }
    }
}
