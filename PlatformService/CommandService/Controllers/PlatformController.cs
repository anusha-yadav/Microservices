using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformController : Controller
    {
        public PlatformController() { }

        [HttpPost]
        public ActionResult TestInBoundConnection()
        {
            Console.WriteLine(" --> Inbound POST # Command Service");
            return Ok("Inbound test of from platforms controller");
        }
    }
}
