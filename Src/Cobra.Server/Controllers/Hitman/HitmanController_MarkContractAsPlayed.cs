using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    /**
     * Invoke: ZContractPlayer::Activate @ 006DFD10
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("MarkContractAsPlayed", HttpMethods.GET, null)]
        public class MarkContractAsPlayedRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("userId", EdmTypes.String)]
            public string UserId { get; set; }

            [NormalizedString]
            [SFunctionParameter("contractId", EdmTypes.String)]
            public string ContractId { get; set; }
        }

        [HttpGet]
        [Route("MarkContractAsPlayed")]
        public IActionResult MarkContractAsPlayed([FromQuery] MarkContractAsPlayedRequest request)
        {
            _hitmanServer.MarkContractAsPlayed(request);

            return Ok();
        }
    }
}
