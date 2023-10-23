using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke:
     * - ZContractManager::ReportContract @ 0044DDF0
     * - ZContractManager::ReportContract @ 0049E220
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("ReportContract", HttpMethods.GET, null)]
        public class ReportContractRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }

            [NormalizedString]
            [SFunctionParameter("contractid", EdmTypes.String)]
            public string ContractId { get; set; }

            [SFunctionParameter("reason", EdmTypes.Int32)]
            public int Reason { get; set; }
        }

        [HttpGet]
        [Route("ReportContract")]
        public IActionResult ReportContract([FromQuery] ReportContractRequest request)
        {
            _hitmanServer.ReportContract(request);

            return Ok();
        }
    }
}
