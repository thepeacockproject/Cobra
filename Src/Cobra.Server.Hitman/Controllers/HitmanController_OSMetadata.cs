using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    public partial class HitmanController
    {
        [HttpGet]
        [Route("$os_metadata")]
        public IActionResult Metadata()
        {
            return JsonGenericResponse(_hitmanMetadataService.GetMetadata());
        }
    }
}
