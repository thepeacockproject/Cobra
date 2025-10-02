using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZMessageManager::MarkMessageAsRead @ 0083A8A0
     * Callback: None
     */
    public partial class HitmanController
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
            _hitmanServer.SetMessageReadStatus(request);

            return Ok();
        }
    }
}
