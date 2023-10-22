using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Sniper
{
    public partial class SniperController
    {
        [HttpGet]
        [Route("$os_metadata")]
        public IActionResult Metadata()
        {
            return JsonGenericResponse(_metadataService.GetMetadata());
        }
    }
}
