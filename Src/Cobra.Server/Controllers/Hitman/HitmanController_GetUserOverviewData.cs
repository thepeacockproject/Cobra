using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
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
            HttpMethods.GET,
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
        public IActionResult GetUserOverviewData([FromQuery] GetUserOverviewDataRequest request)
        {
            return JsonEntryResponse(_hitmanServer.GetUserOverviewData(request));
        }
    }
}
