using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Hitman
{
    public partial class HitmanController
    {
        [HttpGet]
        [Route("$os_metadata")]
        public IActionResult Metadata()
        {
            return JsonGenericResponse(_metadataService.GetMetadata());
        }
    }
}
