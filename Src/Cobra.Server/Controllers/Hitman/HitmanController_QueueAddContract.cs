using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    /**
     * Invoke: ZContractManager::AddContractToQueue @ 004DCF90
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("QueueAddContract", HttpMethods.GET, null)]
        public class QueueAddContractRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("contractid", EdmTypes.String)]
            public string ContractId { get; set; }

            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }
        }

        [HttpGet]
        [Route("QueueAddContract")]
        public IActionResult QueueAddContract([FromQuery] QueueAddContractRequest request)
        {
            _hitmanServer.QueueAddContract(request);

            return Ok();
        }
    }
}
