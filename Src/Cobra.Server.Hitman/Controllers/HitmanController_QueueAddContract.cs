using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
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
