using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    /**
     * Invoke: ZContractManager::ChangeLevel @ 00646AD0
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("QueueRemoveContract", HttpMethods.GET, null)]
        public class QueueRemoveContractRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("contractid", EdmTypes.String)]
            public string ContractId { get; set; }

            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }
        }

        [HttpGet]
        [Route("QueueRemoveContract")]
        public IActionResult QueueRemoveContract([FromQuery] QueueRemoveContractRequest request)
        {
            _hitmanServer.QueueRemoveContract(request);

            return Ok();
        }
    }
}
