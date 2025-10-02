using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZContractManager::UpdateCompetition @ 006E50B0
     * Callback: None
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("CreateCompetition", HttpMethods.Get, null)]
        public class CreateCompetitionRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("fromId", EdmTypes.String)]
            public string FromId { get; set; }

            [SplitNormalizedString]
            [SFunctionParameter("participants", EdmTypes.String)]
            public List<string> Participants { get; set; }

            [NormalizedString]
            [SFunctionParameter("contractId", EdmTypes.String)]
            public string ContractId { get; set; }

            [SFunctionParameter("competitionLength", EdmTypes.Int32)]
            public int CompetitionLength { get; set; }

            [SFunctionParameter("allowInvites", EdmTypes.Boolean)]
            public bool AllowInvites { get; set; }
        }

        [HttpGet]
        [Route("CreateCompetition")]
        public IActionResult CreateCompetition([FromQuery] CreateCompetitionRequest request)
        {
            _hitmanServer.CreateCompetition(request);

            return Ok();
        }
    }
}
