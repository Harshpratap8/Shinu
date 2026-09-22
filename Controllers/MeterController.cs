using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModernMeterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeterController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                message = "Meter API is working"
            });
        }
    }
}
