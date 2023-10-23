using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZContractManager::GetUserWallet @ 00525090
     * Callback: ZContractManager::GetUserWalletCallback @ 008BBBC0
     * ReturnType: ZOServiceOperationResult
     */
    public partial class HitmanController
    {
        [EdmFunctionImport(
            "GetUserWallet",
            HttpMethods.GET,
            null
        )]
        public class GetUserWalletRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("userId", EdmTypes.String)]
            public string UserId { get; set; }
        }

        [HttpGet]
        [Route("GetUserWallet")]
        public IActionResult GetUserWallet([FromQuery] GetUserWalletRequest request)
        {
            return JsonOperationValueResponse(_hitmanServer.GetUserWallet(request));
        }
    }
}
