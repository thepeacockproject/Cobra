using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Sniper.Controllers
{
    public partial class SniperController
    {
        [HttpGet]
        [Route("$os_metadata")]
        public IActionResult Metadata()
        {
            return JsonGenericResponse(_sniperMetadataService.GetMetadata());
        }
    }
}
