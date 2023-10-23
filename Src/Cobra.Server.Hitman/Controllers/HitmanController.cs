using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Controllers;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Cobra.Server.Hitman.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    [Route("hm5")]
    public partial class HitmanController : BaseEdmController
    {
        public abstract class BaseGetScoresRequest : IEdmFunctionImport
        {
            [SFunctionParameter("filter", EdmTypes.Int32)]
            public int Filter { get; set; }

            [SFunctionParameter("startindex", EdmTypes.Int32)]
            public int StartIndex { get; set; }

            [SFunctionParameter("range", EdmTypes.Int32)]
            public int Range { get; set; }

            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }
        }

        public abstract class BaseGetAverageScoresRequest : IEdmFunctionImport
        {
            [NormalizedString]
            [SFunctionParameter("userid", EdmTypes.String)]
            public string UserId { get; set; }
        }

        private const string SchemaNamespace = Constants.SchemaNamespace;

        private readonly IMetadataService _hitmanMetadataService;
        private readonly IHitmanServer _hitmanServer;

        public HitmanController(
            IHitmanMetadataService hitmanMetadataService,
            IHitmanServer hitmanServer
        )
        {
            _hitmanMetadataService = hitmanMetadataService;
            _hitmanServer = hitmanServer;
        }
    }
}
