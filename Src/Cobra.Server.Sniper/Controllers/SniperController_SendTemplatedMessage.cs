using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Sniper.Controllers
{
    /**
     * Invoke:
     * - ZMessageManager::SendMessageToUsersFriends @ 007FF900
     * - ZMessageManager::SendMessageToSelf @ 007FFCC0
     * Callback: None
     */
    public partial class SniperController
    {
        [EdmFunctionImport("SendTemplatedMessage", HttpMethods.Get, null)]
        public class SendTemplatedMessageRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("fromId", EdmTypes.String)]
            public string FromId { get; set; }

            [NormalizedString]
            [SFunctionParameter("toUserId", EdmTypes.String)]
            public string ToUserId { get; set; }

            [SFunctionParameter("tabgroup", EdmTypes.Int32)]
            public int TabGroup { get; set; }

            [SFunctionParameter("category", EdmTypes.Int32)]
            public int Category { get; set; }

            [SFunctionParameter("templateId", EdmTypes.Int32)]
            public int TemplateId { get; set; }

            [NormalizedString]
            [SFunctionParameter("data", EdmTypes.String)]
            public string Data { get; set; }
        }

        [HttpGet]
        [Route("SendTemplatedMessage")]
        public IActionResult SendTemplatedMessage([FromQuery] SendTemplatedMessageRequest request)
        {
            //NOTE: toUserId will be "----Friends----" if it's called from SendMessageToUsersFriends
            //NOTE: tabgroup will be "1" if it's called from SendMessageToSelf
            //NOTE: data might contain data that needs additional parsing
            _sniperServer.SendTemplatedMessage(request);

            return Ok();
        }
    }
}
