﻿using Cobra.Server.Edm.Attributes;
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
        [EdmFunctionImport("InviteToCompetition", HttpMethods.Get, null)]
        public class InviteToCompetitionRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("fromId", EdmTypes.String)]
            public string FromId { get; set; }

            [SplitNormalizedString]
            [SFunctionParameter("participants", EdmTypes.String)]
            public List<string> Participants { get; set; }

            //NOTE: This can be a Contract Id and should therefor be a string
            [NormalizedString]
            [SFunctionParameter("competitionId", EdmTypes.String)]
            public string CompetitionId { get; set; }
        }

        [HttpGet]
        [Route("InviteToCompetition")]
        public IActionResult InviteToCompetition([FromQuery] InviteToCompetitionRequest request)
        {
            _hitmanServer.InviteToCompetition(request);

            return Ok();
        }
    }
}
