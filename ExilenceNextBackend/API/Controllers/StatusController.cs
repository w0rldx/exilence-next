namespace API.Controllers
{
    using System.Threading.Tasks;
    using API.Hubs;
    using API.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;

    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHubContext<BaseHub> _hubContext;

        public StatusController(IConfiguration configuration, IHubContext<BaseHub> hubContext)
        {
            _hubContext = hubContext;
            _configuration = configuration;
        }

        //Used by HAProxy to detect downtime.
        [Route("")]
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok();
        }

        [Route("announcement")]
        [HttpPost]
        public async Task<IActionResult> Announcement(AnouncementModel announcement)
        {
            var password = _configuration.GetSection("Announcement")["Password"];
            var message = new AnouncementMessageModel
            {
                Title = announcement.Title,
                Message = announcement.Message
            };

            if (password == null || announcement.Password != password)
            {
                return BadRequest(new
                {
                    result = "Wrong password"
                });
            }

            await _hubContext.Clients.All.SendAsync("OnAnnouncement", message);
            return Ok(new
            {
                result = "Message sent"
            });
        }
    }
}
