using Cobra.Server.Edm.Controllers;
using Cobra.Server.Edm.Interfaces;
using Cobra.Server.Sniper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Cobra.Server.Sniper.Controllers
{
    [Route("sniper")]
    public partial class SniperController : BaseEdmController
    {
        private const string SchemaNamespace = Constants.SchemaNamespace;

        private readonly IMetadataService _sniperMetadataService;
        private readonly ISniperServer _sniperServer;

        public SniperController(
            [FromKeyedServices(Constants.SchemaNamespace)] IMetadataService sniperMetadataService,
            ISniperServer sniperServer
        )
        {
            _sniperMetadataService = sniperMetadataService;
            _sniperServer = sniperServer;
        }
    }
}
