using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers.Sniper
{
    public partial class SniperController
    {
        [HttpPost]
        [Route("AddMetrics")]
        public IActionResult AddMetrics([FromBody] JsonDocument request)
        {
            return Ok();
        }
    }
}
