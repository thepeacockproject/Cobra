using Cobra.Server.Attributes;
using Cobra.Server.Enums;
using Cobra.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
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
