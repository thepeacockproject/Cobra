using Microsoft.AspNetCore.Mvc;

namespace Cobra.Server.Controllers
{
    public class ErrorController : Controller
    {
        [Route("{*url}")]
        public IActionResult CatchAll(string url)
        {
            return NotFound();
        }
    }
}
