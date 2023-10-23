using Cobra.Server.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Hitman.Controllers
{
    public partial class HitmanController
    {
        [HttpGet]
        [Route("os_getStatus")]
        public IActionResult GetStatus()
        {
            return JsonGenericResponse(new OSGetStatus
            {
                ClientIP = "127.0.0.1"
            });
        }
    }
}
