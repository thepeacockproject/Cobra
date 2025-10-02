﻿using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZContractManager::RetrieveUserOverviewData @ 006DF920
     * Callback: ZContractManager::RetrieveUserOverviewDataCallback @ 00554640
     * ReturnType: ZOEntry
     */
    public partial class HitmanController
    {
        [EdmFunctionImport(
            "GetUserOverviewData",
            HttpMethods.Get,
            $"{SchemaNamespace}.GetUserOverviewData"
        )]
        public class GetUserOverviewDataRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }
        }

        [HttpGet]
        [Route("GetUserOverviewData")]
        public async Task<IActionResult> GetUserOverviewData([FromQuery] GetUserOverviewDataRequest request)
        {
            return JsonEntryResponse(await _hitmanServer.GetUserOverviewData(request));
        }
    }
}
