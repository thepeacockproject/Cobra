using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Sniper.Controllers
{
    /**
     * Invoke: ZMessageManager::MarkMessageAsRead @ 00800010
     * Callback: None
     */
    public partial class SniperController
    {
        [EdmFunctionImport("SetMessageReadStatus", HttpMethods.Get, null)]
        public class SetMessageReadStatusRequest : IEdmFunctionImport
        {
            [SFunctionParameter("messageId", EdmTypes.Int32)]
            public int MessageId { get; set; }

            [SFunctionParameter("isRead", EdmTypes.Boolean)]
            public bool IsRead { get; set; }
        }

        [HttpGet]
        [Route("SetMessageReadStatus")]
        public IActionResult SetMessageReadStatus([FromQuery] SetMessageReadStatusRequest request)
        {
            //NOTE: isRead will always be set to "true"
            _sniperServer.SetMessageReadStatus(request);

            return Ok();
        }
    }
}
