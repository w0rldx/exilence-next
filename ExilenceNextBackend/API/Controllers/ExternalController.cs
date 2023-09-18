namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ExternalController : ControllerBase
    {
        //Used by HAProxy to detect downtime. 
        [Route("")]
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok();
        }
    }
}
