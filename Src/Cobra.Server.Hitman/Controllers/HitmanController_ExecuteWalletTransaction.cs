﻿using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    /**
     * Invoke: ZContractManager::ExecuteWalletTransactionAsync @ 00684EF0
     * Callback: ZContractManager::ExecuteWalletTransactionAsyncCallback @ 005E88A0
     * ReturnType: ZOServiceOperationResult
     */
    public partial class HitmanController
    {
        [EdmFunctionImport("ExecuteWalletTransaction", HttpMethods.Get, null)]
        public class ExecuteWalletTransactionRequest : IEdmFunctionImport
        {
            [SFunctionParameter("amount", EdmTypes.Int32)]
            public int Amount { get; set; }

            [NormalizedString]
            [SFunctionParameter("userId", EdmTypes.String)]
            public string UserId { get; set; }

            [SFunctionParameter("tokenId", EdmTypes.Int32)]
            public int TokenId { get; set; }

            [SFunctionParameter("subId", EdmTypes.Int32)]
            public int SubId { get; set; }

            [SFunctionParameter("level", EdmTypes.Int32)]
            public int Level { get; set; }
        }

        [HttpGet]
        [Route("ExecuteWalletTransaction")]
        public IActionResult ExecuteWalletTransaction([FromQuery] ExecuteWalletTransactionRequest request)
        {
            //NOTE: Appears to be the new WalletAmount left
            return JsonOperationValueResponse(_hitmanServer.ExecuteWalletTransaction(request));
        }
    }
}
