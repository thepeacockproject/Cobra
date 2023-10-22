using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    /**
     * Invoke: ZContractManager::Start @ 806E20
     * Callback: ZContractManager::RetrieveFeaturedContractCallback @ 964FE0
     * ReturnType: ZOEntry
     */
    public partial class HitmanController
    {
        [EdmFunctionImport(
            "GetFeaturedContract",
            HttpMethods.GET,
            $"{SchemaNamespace}.Contract"
        )]
        public class GetFeaturedContractRequest : IEdmFunctionImport
        {
            [SFunctionParameter("levelindex", EdmTypes.Int32)]
            public int LevelIndex { get; set; }

            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }
        }

        [HttpGet]
        [Route("GetFeaturedContract")]
        public IActionResult GetFeaturedContract([FromQuery] GetFeaturedContractRequest request)
        {
            return JsonEntryResponse(_hitmanServer.GetFeaturedContract(request));
        }
    }
}