using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalance.Web.Controllers
{

    [ApiController]
    public class HealthController : ControllerBase
    {
        [Route("Health/Heartbeat")]
        public IActionResult Heartbeat()
        {
            return Ok();      
        }
    }
}
