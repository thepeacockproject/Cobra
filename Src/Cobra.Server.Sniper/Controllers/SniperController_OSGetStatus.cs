using Cobra.Server.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Sniper.Controllers
{
    public partial class SniperController
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
