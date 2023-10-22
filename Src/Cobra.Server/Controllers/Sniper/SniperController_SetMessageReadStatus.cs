using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Sniper
{
    /**
     * Invoke: ZMessageManager::MarkMessageAsRead @ 00800010
     * Callback: None
     */
    public partial class SniperController
    {
        [EdmFunctionImport("SetMessageReadStatus", HttpMethods.GET, null)]
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
            return Ok();
        }
    }
}
