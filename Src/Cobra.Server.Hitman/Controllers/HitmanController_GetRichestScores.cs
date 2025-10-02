﻿using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZLeaderboard::GetSpecialRichest @ 00868C30
     * Callback: ZLeaderboard::GetScoresCallback @ 007FD3D0
     * ReturnType: ZOFeed
     */
    public partial class HitmanController
    {
        [EdmFunctionImport(
            "GetRichestScores",
            HttpMethods.Get,
            $"Collection({SchemaNamespace}.ScoreEntry)"
        )]
        public class GetRichestScoresRequest : BaseGetScoresRequest
        {
            //Do nothing
        }

        [HttpGet]
        [Route("GetRichestScores")]
        public IActionResult GetRichestScores([FromQuery] GetRichestScoresRequest request)
        {
            return JsonFeedResponse(_hitmanServer.GetScores(request));
        }
    }
}
